using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VsLocalizedIntellisense.Diff.Models;
using VsLocalizedIntellisense.Diff.Models.Element;
using VsLocalizedIntellisense.Diff.Models.Host;
using VsLocalizedIntellisense.Diff.ViewModels;
using VsLocalizedIntellisense.Diff.Views;

namespace VsLocalizedIntellisense.Diff
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //var appPath = new AppPath();

            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configure => {
                    var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    configure.SetBasePath(applicationDirectory!);
                })
                .ConfigureServices(services => {
                    services.AddHostedService<MainHostedService>();
                    services.AddSingleton<IAppPath, AppPath>();
                    services.AddSingleton<Application, App>();
                    services.AddSingleton<MainElement>();
                    services.AddSingleton<MainViewModel>();
                    services.AddTransient<MainWindow>();
                })

                .Build()
                .Run()
            ;
        }
    }
}
