using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VsLocalizedIntellisense.Models.Mvvm;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.ViewModels.Message;
using VsLocalizedIntellisense.Models.Mvvm.Binding;
using VsLocalizedIntellisense.Models.Mvvm.Command;
using VsLocalizedIntellisense.Models.Mvvm.Message;
using VsLocalizedIntellisense.Models;
using System.ComponentModel.DataAnnotations;
using VsLocalizedIntellisense.Models.Mvvm.Binding.Collection;
using System.ComponentModel;
using VsLocalizedIntellisense.Models.Configuration;
using System.Collections.ObjectModel;
using System.IO;
using VsLocalizedIntellisense.Models.Service.CommandShell;
using System.Windows;
using System.Diagnostics;
using System.Security.Policy;

namespace VsLocalizedIntellisense.ViewModels
{
    public class MainViewModel : SingleModelViewModelBase<MainElement>
    {
        #region variable

        private bool _isDownloading = false;
        private bool _isDownloaded = false;
        private bool _isExecuting = false;

        private string _installCommand = string.Empty;

        private DelegateCommand _selectInstallRootDirectoryPathCommand;
        private AsyncDelegateCommand _downloadCommand;
        private DelegateCommand _backCommand;
        private AsyncDelegateCommand _executeCommand;
        private DelegateCommand _openReleasePageCommand;


        bool _filterTrace;
        bool _filterDebug;
        bool _filterInformation = true;
        bool _filterWarning = true;
        bool _filterError = true;
        bool _filterCritical = true;

        #endregion

        public MainViewModel(MainElement model, ObservableCollection<LogItemElement> stockLogItems, AppConfiguration configuration, ILoggerFactory loggerFactory)
            : base(model, loggerFactory)
        {
            Configuration = configuration;
            DirectoryCollection = new ModelViewModelObservableCollectionManager<DirectoryElement, DirectoryViewModel>(Model.IntellisenseDirectoryElements, new ModelViewModelObservableCollectionOptions<DirectoryElement, DirectoryViewModel>()
            {
                ToViewModel = m => new DirectoryViewModel(m, LoggerFactory),
            });

            StockLogCollection = new ModelViewModelObservableCollectionManager<LogItemElement, LogItemViewModel>(stockLogItems, new ModelViewModelObservableCollectionOptions<LogItemElement, LogItemViewModel>()
            {
                ToViewModel = m => new LogItemViewModel(m, LoggerFactory),
                AddItems = p =>
                {
                    Messenger.Send(new ScrollMessage());
                }
            });

            StockLogItems = StockLogCollection.GetDefaultView();
            StockLogItems.Filter += StockLogItems_Filter;

            PropertyChanged += MainViewModel_PropertyChanged;
        }

        #region property

        [Messenger]
        public Messenger Messenger { get; } = new Messenger();

        private AppConfiguration Configuration { get; }

        private Dictionary<DirectoryElement, IList<FileInfo>> InstallItems { get; } = new Dictionary<DirectoryElement, IList<FileInfo>>();
        private CommandShellEditor GeneratedCommandShellEditor { get; set; }

        public bool FilterTrace
        {
            get => this._filterTrace;
            set => SetVariable(ref this._filterTrace, value);
        }
        public bool FilterDebug
        {
            get => this._filterDebug;
            set => SetVariable(ref this._filterDebug, value);
        }
        public bool FilterInformation
        {
            get => this._filterInformation;
            set => SetVariable(ref this._filterInformation, value);
        }
        public bool FilterWarning
        {
            get => this._filterWarning;
            set => SetVariable(ref this._filterWarning, value);
        }
        public bool FilterError
        {
            get => this._filterError;
            set => SetVariable(ref this._filterError, value);
        }
        public bool FilterCritical
        {
            get => this._filterCritical;
            set => SetVariable(ref this._filterCritical, value);
        }

        public string InstallCommand
        {
            get => this._installCommand;
            set => SetVariable(ref this._installCommand, value);
        }

        [Required(ErrorMessageResourceName = nameof(Properties.Resources.UI_Validation_Required), ErrorMessageResourceType = typeof(Properties.Resources))]
        public string InstallRootDirectoryPath
        {
            get => Model.InstallRootDirectoryPath;
            set => SetModel(value);
        }

        public bool IsDownloading
        {
            get => this._isDownloading;
            set => SetVariable(ref this._isDownloading, value);
        }

        public bool IsDownloaded
        {
            get => this._isDownloaded;
            set => SetVariable(ref this._isDownloaded, value);
        }

        public bool IsExecuting
        {
            get => this._isExecuting;
            set => SetVariable(ref this._isExecuting, value);
        }

        private ModelViewModelObservableCollectionManager<DirectoryElement, DirectoryViewModel> DirectoryCollection { get; }
        public ICollectionView DirectoryItems => DirectoryCollection.GetDefaultView();

        private ModelViewModelObservableCollectionManager<LogItemElement, LogItemViewModel> StockLogCollection { get; }
        public ICollectionView StockLogItems { get; }

        public string AppVersion => Configuration.Replace("${APP:VERSION}");
        public string AppShortRevision => Configuration.Replace("${APP:REVISION:SHORT}");
        public string AppLongRevision => Configuration.Replace("${APP:REVISION:LONG}");

        public static LogLevel LoggerTrace {get;} = LogLevel.Trace;
        public static LogLevel LoggerDebug {get;} = LogLevel.Debug;
        public static LogLevel LoggerInformation {get;} = LogLevel.Information;
        public static LogLevel LoggerWarning {get;} = LogLevel.Warning;
        public static LogLevel LoggerError {get;} = LogLevel.Error;
        public static LogLevel LoggerCritical { get; } = LogLevel.Critical;

        #endregion

        #region command

        public ICommand SelectInstallRootDirectoryPathCommand
        {
            get
            {
                if (this._selectInstallRootDirectoryPathCommand == null)
                {
                    this._selectInstallRootDirectoryPathCommand = new DelegateCommand(
                        o =>
                        {
                            var message = new OpenFileDialogMessage()
                            {
                                Kind = OpenFileDialogKind.Directory,
                                CurrentDirectory = IOHelper.GetPhysicalDirectory(InstallRootDirectoryPath),
                            };
                            Messenger.Send(message);

                            if (message.ResultDirectory != null)
                            {
                                InstallRootDirectoryPath = message.ResultDirectory.FullName;
                            }
                        },
                        _ => !IsDownloading && !IsExecuting
                    );
                }
                return this._selectInstallRootDirectoryPathCommand;
            }
        }

        public ICommand DownloadCommand
        {
            get
            {
                if (this._downloadCommand == null)
                {
                    this._downloadCommand = new AsyncDelegateCommand(
                        async _ =>
                        {
                            if (IsDownloading || IsExecuting)
                            {
                                Logger.LogInformation("disable download");
                                return;
                            }

                            IsDownloaded = false;
                            IsDownloading = true;
                            try
                            {
                                var items = await Model.DownloadIntellisenseFilesAsync();
                                InstallItems.Clear();
                                foreach (var pair in items)
                                {
                                    InstallItems.Add(pair.Key, pair.Value);
                                }
                                GeneratedCommandShellEditor = Model.GenerateShellCommand(InstallItems);
                                InstallCommand = GeneratedCommandShellEditor.ToSourceCode();
                                IsDownloaded = true;
                            }
                            finally
                            {
                                //TODO: 頑張ったけどダウンロードボタンがダウンロード直後に死ぬけどもうどうでもいいわ
                                IsDownloading = false;
                            }
                        }
                    );
                }
                return this._downloadCommand;
            }
        }

        public ICommand BackCommand
        {
            get
            {
                if (this._backCommand == null)
                {
                    this._backCommand = new DelegateCommand(
                        _ =>
                        {
                            foreach (var dir in DirectoryCollection.ViewModels)
                            {
                                dir.ResetPercent();
                            }
                            IsDownloaded = false;
                        }
                    );
                }

                return this._backCommand;
            }
        }

        public ICommand ExecuteCommand
        {
            get
            {
                if (this._executeCommand == null)
                {
                    this._executeCommand = new AsyncDelegateCommand(
                        async _ =>
                        {
                            IsExecuting = true;
                            try
                            {
                                await Model.ExecuteCommandShellAsync(GeneratedCommandShellEditor);
                            }
                            finally
                            {
                                IsExecuting = false;
                            }
                        },
                        _ => IsDownloaded && !IsExecuting
                    );
                }
                return this._executeCommand;
            }
        }

        public ICommand OpenReleasePageCommand
        {
            get
            {
                if (this._openReleasePageCommand == null)
                {
                    this._openReleasePageCommand = new DelegateCommand(
                        _ =>
                        {
                            var uri = Configuration.GetReleaseUri();
                            var psi = new ProcessStartInfo()
                            {
                                FileName = uri.ToString(),
                                UseShellExecute = true,
                            };
                            Process.Start(psi);
                        }
                    );
                }

                return this._openReleasePageCommand;
            }
        }

        #endregion

        #region function

        public void RefreshCommand()
        {
            this._selectInstallRootDirectoryPathCommand?.RaiseCanExecuteChanged();
            this._downloadCommand?.RaiseCanExecuteChanged();
            this._backCommand?.RaiseCanExecuteChanged();
            this._executeCommand?.RaiseCanExecuteChanged();
        }

        #endregion

        #region SingleModelViewModelBase

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (StockLogItems != null)
                {
                    StockLogItems.Filter -= StockLogItems_Filter;
                }
                PropertyChanged -= MainViewModel_PropertyChanged;

            }
            base.Dispose(disposing);
        }

        #endregion

        private bool StockLogItems_Filter(object obj)
        {
            if (!(obj is LogItemViewModel item))
            {
                return false;
            }

            var filter = false;

            if (FilterTrace)
            {
                filter |= item.Level == LogLevel.Trace;
            }
            if (FilterDebug)
            {
                filter |= item.Level == LogLevel.Debug;
            }
            if (FilterInformation)
            {
                filter |= item.Level == LogLevel.Information;
            }
            if (FilterWarning)
            {
                filter |= item.Level == LogLevel.Warning;
            }
            if (FilterError)
            {
                filter |= item.Level == LogLevel.Error;
            }
            if (FilterCritical)
            {
                filter |= item.Level == LogLevel.Critical;
            }

            return filter;
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var commandPropertyNames = new[]
            {
                nameof(IsDownloading),
                nameof(IsDownloaded),
                nameof(IsExecuting),
            };
            if (commandPropertyNames.Contains(e.PropertyName))
            {
                RefreshCommand();
            }

            var logPropertyNames = new[]
            {
                nameof(FilterTrace),
                nameof(FilterDebug),
                nameof(FilterInformation),
                nameof(FilterWarning),
                nameof(FilterError),
                nameof(FilterCritical),
            };
            if (logPropertyNames.Contains(e.PropertyName))
            {
                StockLogItems.Refresh();
            }
        }
    }
}
