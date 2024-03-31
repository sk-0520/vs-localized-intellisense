using System.Configuration;
using System.Data;
using System.Windows;
using VsLocalizedIntellisense.Diff.Models.Element;
using VsLocalizedIntellisense.Diff.ViewModels;
using VsLocalizedIntellisense.Diff.Views;

namespace VsLocalizedIntellisense.Diff
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: Application
    {
        #region Application

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var model = new MainElement();
            var viewModel = new MainViewModel(model);

            var view = new MainWindow() {
                DataContext = viewModel,
            };
            MainWindow = view;
            MainWindow.Show();
        }

        #endregion
    }
}
