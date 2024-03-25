using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using VsLocalizedIntellisense.Models;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;
using VsLocalizedIntellisense.Models.Mvvm.Binding.Collection;
using VsLocalizedIntellisense.Models.Mvvm.Command;
using VsLocalizedIntellisense.Models.Mvvm.Message;
using VsLocalizedIntellisense.Models.Service.CommandShell;
using VsLocalizedIntellisense.ViewModels.Message;
using VsLocalizedIntellisense.Models.Service.Application;
using System.Windows;

namespace VsLocalizedIntellisense.ViewModels
{
    public class MainViewModel: SingleModelViewModelBase<MainElement>
    {
        #region variable

        ViewModelBase _contextContent;
        ContextMode _contextMode;

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

            this._contextMode = Model.HasIntellisenseVersionItems
                ? ContextMode.Download
                : ContextMode.Refresh
            ;


            DirectoryCollection = new ModelViewModelObservableCollectionManager<DirectoryElement, DirectoryViewModel>(Model.IntellisenseDirectoryElements, new ModelViewModelObservableCollectionOptions<DirectoryElement, DirectoryViewModel>() {
                ToViewModel = m => new DirectoryViewModel(m, LoggerFactory),
            });

            StockLogCollection = new ModelViewModelObservableCollectionManager<LogItemElement, LogItemViewModel>(stockLogItems, new ModelViewModelObservableCollectionOptions<LogItemElement, LogItemViewModel>() {
                ToViewModel = m => new LogItemViewModel(m, LoggerFactory),
                AddItems = p => {
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

        public ViewModelBase ContextContent
        {
            get
            {
                if(this._contextContent != null) {
                    this._contextContent.Dispose();
                    this._contextContent = null;
                }

                switch(ContextMode) {
                    case ContextMode.Refresh:
                        this._contextContent = new RefreshViewModel(Model, InstallItems, m => ChangedContext(m), Messenger, Configuration, LoggerFactory);
                        break;

                    case ContextMode.Download:
                        foreach(var installItem in InstallItems) {
                            installItem.Key.DownloadPercent = 0;
                        }
                        this._contextContent = new DownloadViewModel(Model, InstallItems, m => ChangedContext(m), Messenger, Configuration, LoggerFactory);
                        break;

                    case ContextMode.Install:
                        this._contextContent = new InstallViewModel(Model, InstallItems, m => ChangedContext(m), Messenger, Configuration, LoggerFactory);
                        break;

                    default:
                        throw new NotImplementedException();
                }

                return this._contextContent;
            }
        }
        public ContextMode ContextMode
        {
            get => this._contextMode;
            set => SetVariable(ref this._contextMode, value);
        }

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

        private ModelViewModelObservableCollectionManager<DirectoryElement, DirectoryViewModel> DirectoryCollection { get; }
        public ICollectionView DirectoryItems => DirectoryCollection.GetDefaultView();

        private ModelViewModelObservableCollectionManager<LogItemElement, LogItemViewModel> StockLogCollection { get; }
        public ICollectionView StockLogItems { get; }

        public string AppVersion => Configuration.Replace("${APP:VERSION}");
        public string AppShortRevision => Configuration.Replace("${APP:REVISION:SHORT}");
        public string AppLongRevision => Configuration.Replace("${APP:REVISION:LONG}");

        public static LogLevel LoggerTrace { get; } = LogLevel.Trace;
        public static LogLevel LoggerDebug { get; } = LogLevel.Debug;
        public static LogLevel LoggerInformation { get; } = LogLevel.Information;
        public static LogLevel LoggerWarning { get; } = LogLevel.Warning;
        public static LogLevel LoggerError { get; } = LogLevel.Error;
        public static LogLevel LoggerCritical { get; } = LogLevel.Critical;

        #endregion

        #region command







        public ICommand OpenReleasePageCommand => this._openReleasePageCommand ??= new DelegateCommand(
            _ => {
                var uri = Configuration.GetReleaseUri();
                var psi = new ProcessStartInfo() {
                    FileName = uri.ToString(),
                    UseShellExecute = true,
                };
                Process.Start(psi);
            }
        );

        #endregion

        #region function

        private void ChangedContext(ContextMode mode)
        {
            ContextMode = mode;
        }

        #endregion

        #region SingleModelViewModelBase

        protected override void Dispose(bool disposing)
        {
            if(!IsDisposed) {
                if(StockLogItems != null) {
                    StockLogItems.Filter -= StockLogItems_Filter;
                }
                PropertyChanged -= MainViewModel_PropertyChanged;

            }
            base.Dispose(disposing);
        }

        #endregion

        private bool StockLogItems_Filter(object obj)
        {
            if(!(obj is LogItemViewModel item)) {
                return false;
            }

            var filter = false;

            if(FilterTrace) {
                filter |= item.Level == LogLevel.Trace;
            }
            if(FilterDebug) {
                filter |= item.Level == LogLevel.Debug;
            }
            if(FilterInformation) {
                filter |= item.Level == LogLevel.Information;
            }
            if(FilterWarning) {
                filter |= item.Level == LogLevel.Warning;
            }
            if(FilterError) {
                filter |= item.Level == LogLevel.Error;
            }
            if(FilterCritical) {
                filter |= item.Level == LogLevel.Critical;
            }

            return filter;
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var logPropertyNames = new[]
            {
                nameof(FilterTrace),
                nameof(FilterDebug),
                nameof(FilterInformation),
                nameof(FilterWarning),
                nameof(FilterError),
                nameof(FilterCritical),
            };
            if(logPropertyNames.Contains(e.PropertyName)) {
                StockLogItems.Refresh();
            }

            if(e.PropertyName == nameof(ContextMode)) {
                RaisePropertyChanged(nameof(ContextContent));
            }
        }
    }
}
