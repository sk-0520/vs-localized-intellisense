using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VsLocalizedIntellisense.Models;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Data;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;
using VsLocalizedIntellisense.Models.Mvvm.Binding.Collection;
using VsLocalizedIntellisense.Models.Mvvm.Command;
using VsLocalizedIntellisense.Models.Mvvm.Message;
using VsLocalizedIntellisense.Models.Service.Application;

namespace VsLocalizedIntellisense.ViewModels
{
    public class RefreshViewModel: ContextContentViewModelBase<MainElement>
    {
        #region variable

        private bool _isDownloading;

        private AsyncDelegateCommand _refreshCommand;

        #endregion

        public RefreshViewModel(MainElement model, IDictionary<DirectoryElement, IList<FileInfo>> installItems, Action<ContextMode> changedCallback, ISendableMessenger messenger, AppConfiguration configuration, ILoggerFactory loggerFactory)
            : base(model, changedCallback, messenger, configuration, loggerFactory)
        { }

        #region property

        public bool IsDownloading
        {
            get => this._isDownloading;
            set => SetVariable(ref this._isDownloading, value);
        }

        #endregion

        #region command

        [ObserveProperty(nameof(IsDownloading))]
        public ICommand RefreshCommand => this._refreshCommand ??= new AsyncDelegateCommand(
            async _ => {
                if(IsDownloading) {
                    Logger.LogInformation("disable download");
                    return;
                }

                IsDownloading = true;
                try {
                    var appIntellisensePageService = new AppIntellisensePageService(Configuration, LoggerFactory);
                    var appFileService = new AppFileService(Configuration, LoggerFactory);
                    var intellisenseVersionItems = await appIntellisensePageService.GetDataListAsync("intellisense");
                    var intellisenseVersionData = new IntellisenseVersionData {
                        VersionItems = intellisenseVersionItems.Directories,
                    };
                    appFileService.SaveIntellisenseVersionData(intellisenseVersionData);

                    // ちょっとこのためにデータ詰め直しとか関係各所のモデルやらに伝搬するのしんどいので再起動
                    AppHelper.Reboot();
                } catch(Exception ex) {
                    Logger.LogError(ex.ToString());
                } finally {
                    IsDownloading = false;
                }
            },
            _ => !IsDownloading
        );

        #endregion
    }
}
