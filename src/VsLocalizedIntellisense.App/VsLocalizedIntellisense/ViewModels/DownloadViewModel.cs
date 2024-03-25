using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VsLocalizedIntellisense.Models;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;
using VsLocalizedIntellisense.Models.Mvvm.Binding.Collection;
using VsLocalizedIntellisense.Models.Mvvm.Command;
using VsLocalizedIntellisense.Models.Mvvm.Message;
using VsLocalizedIntellisense.ViewModels.Message;

namespace VsLocalizedIntellisense.ViewModels
{
    public class DownloadViewModel: ContextContentViewModelBase<MainElement>
    {
        #region variable

        private bool _isDownloading;

        private DelegateCommand _selectInstallRootDirectoryPathCommand;
        private AsyncDelegateCommand _downloadCommand;

        #endregion

        public DownloadViewModel(MainElement model, IDictionary<DirectoryElement, IList<FileInfo>> installItems, Action<ContextMode> changedCallback, ISendableMessenger messenger, AppConfiguration configuration, ILoggerFactory loggerFactory)
            : base(model, changedCallback, messenger, configuration, loggerFactory)
        {
            InstallItems = installItems;

            DirectoryCollection = new ModelViewModelObservableCollectionManager<DirectoryElement, DirectoryViewModel>(Model.IntellisenseDirectoryElements, new ModelViewModelObservableCollectionOptions<DirectoryElement, DirectoryViewModel>() {
                ToViewModel = m => new DirectoryViewModel(m, LoggerFactory),
            });

            AddCommandHook(
                SelectInstallRootDirectoryPathCommand,
                new[] {
                    nameof(IsDownloading),
                }
            );

            AddCommandHook(
                DownloadCommand,
                new[] {
                    nameof(IsDownloading),
                }
            );
        }

        #region property

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

        private IDictionary<DirectoryElement, IList<FileInfo>> InstallItems { get; }

        private ModelViewModelObservableCollectionManager<DirectoryElement, DirectoryViewModel> DirectoryCollection { get; }
        public ICollectionView DirectoryItems => DirectoryCollection.GetDefaultView();


        #endregion

        #region command

        public ICommand SelectInstallRootDirectoryPathCommand => this._selectInstallRootDirectoryPathCommand ??= new DelegateCommand(
            o => {
                var message = new OpenFileDialogMessage() {
                    Kind = OpenFileDialogKind.Directory,
                    CurrentDirectory = IOHelper.GetPhysicalDirectory(InstallRootDirectoryPath),
                };
                Messenger.Send(message);

                if(message.ResultDirectory != null) {
                    InstallRootDirectoryPath = message.ResultDirectory.FullName;
                }
            },
            _ => !IsDownloading
        );

        public ICommand DownloadCommand => this._downloadCommand ??= new AsyncDelegateCommand(
            async _ => {
                if(IsDownloading) {
                    Logger.LogInformation("disable download");
                    return;
                }

                IsDownloading = true;
                try {
                    var items = await Model.DownloadIntellisenseFilesAsync();
                    InstallItems.Clear();
                    foreach(var pair in items) {
                        InstallItems.Add(pair.Key, pair.Value);
                    }
                    ChangeMode(ContextMode.Install);
                } catch (Exception ex) {
                    Logger.LogError(ex.ToString());
                } finally {
                    IsDownloading = false;
                }
            },
            _ => !IsDownloading
        );


        #endregion

        #region function

        #endregion
    }
}
