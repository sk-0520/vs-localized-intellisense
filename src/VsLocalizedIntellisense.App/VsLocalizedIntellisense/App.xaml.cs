using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VsLocalizedIntellisense.Models;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Data;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Service.Application;
using VsLocalizedIntellisense.Models.Service.GitHub;
using VsLocalizedIntellisense.ViewModels;
using VsLocalizedIntellisense.Views;

namespace VsLocalizedIntellisense
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        #region property

        private ILogger Logger { get; set; }

        #endregion

        #region Application

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var appConfiguration = new AppConfiguration(new AppConfigurationInitializeParameters(DateTime.UtcNow));
            var stockLogItems = new ObservableCollection<LogItemElement>();
            var loggerFactory = Logging.Initialize(appConfiguration, stockLogItems);
            Logger = loggerFactory.CreateLogger(GetType());
            Logger.LogInformation("START");

            var appFileService = new AppFileService(appConfiguration, loggerFactory);
            var appGitHubService = new AppGitHubService(appConfiguration, loggerFactory);
            var intellisenseVersionData = appFileService.GetIntellisenseVersionData();
            if (intellisenseVersionData != null)
            {
                Logger.LogInformation("キャッシュからインテリセンスバージョンデータ取得");
            }
            else
            {
                Logger.LogInformation("GitHubからインテリセンスバージョンデータ取得");
                var intellisenseVersionItems = await appGitHubService.GetIntellisenseVersionItemsAsync(appConfiguration.GetRepositoryRevision());
                intellisenseVersionData = new IntellisenseVersionData
                {
                    VersionItems = intellisenseVersionItems.ToArray()
                };
                appFileService.SaveIntellisenseVersionData(intellisenseVersionData);
            }
            if (intellisenseVersionData == null)
            {
                Logger.LogError("インテリセンスバージョンデータ取得失敗");
                throw new Exception(nameof(intellisenseVersionData));
            }

            var mainElement = new MainElement(appConfiguration, intellisenseVersionData.VersionItems, appFileService, appGitHubService, loggerFactory);

            var mainViewModel = new MainViewModel(mainElement, stockLogItems, appConfiguration, loggerFactory);
            var mainView = new MainWindow();

            MainWindow = mainView;
            MainWindow.DataContext = mainViewModel;

            Logger.LogTrace("ビュー起動: こんにちはGUI");

#if DEBUG
            Logger.LogTrace("LogTrace");
            Logger.LogDebug("LogDebug");
            Logger.LogInformation("LogInformation");
            Logger.LogWarning("LogWarning");
            Logger.LogError("LogError");
            Logger.LogCritical("LogCritical");
#endif

            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Logger?.LogInformation("EXIT");
            Logging.Shutdown();
        }

        #endregion
    }
}
