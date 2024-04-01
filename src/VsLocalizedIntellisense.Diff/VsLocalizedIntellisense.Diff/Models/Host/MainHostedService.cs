using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;
using VsLocalizedIntellisense.Diff.Models.Element;
using VsLocalizedIntellisense.Diff.ViewModels;
using VsLocalizedIntellisense.Diff.Views;

namespace VsLocalizedIntellisense.Diff.Models.Host
{
    public class MainHostedService: IHostedService
    {
        public MainHostedService(IHostApplicationLifetime hostApplicationLifetime, Application application, MainWindow view, MainElement mainElement, MainViewModel mainViewModel)
        {
            HostApplicationLifetime = hostApplicationLifetime;
            Application = application;
            View = view;
            MainElement = mainElement;
            MainViewModel = mainViewModel;
        }

        #region property

        private IHostApplicationLifetime HostApplicationLifetime { get; }
        private Application Application { get; }
        private MainWindow View { get; }
        private MainElement MainElement { get; }
        private MainViewModel MainViewModel { get; }

        #endregion

        #region IHostedService

        public Task StartAsync(CancellationToken cancellationToken)
        {
            View.DataContext = MainViewModel;
            Application.Run(View);
            HostApplicationLifetime.StopApplication();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
