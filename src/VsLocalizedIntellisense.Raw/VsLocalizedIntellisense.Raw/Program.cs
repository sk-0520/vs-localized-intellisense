using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VsLocalizedIntellisense.Raw.Models.Host;

namespace VsLocalizedIntellisense.Raw
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => {
                    services.AddHostedService<ApplicationHostedService>();
                })
                .Build()
                .RunAsync()
            ;
        }
    }
}
