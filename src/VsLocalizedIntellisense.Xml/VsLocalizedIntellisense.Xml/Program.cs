using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VsLocalizedIntellisense.Xml.Models.Host;

namespace VsLocalizedIntellisense.Xml
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configure => {
                    var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    configure.SetBasePath(applicationDirectory!);
                })
                .ConfigureServices(services => {
                    services.AddHostedService<ApplicationHostedService>();
                })
                .Build()
                .RunAsync()
            ;
        }
    }
}
