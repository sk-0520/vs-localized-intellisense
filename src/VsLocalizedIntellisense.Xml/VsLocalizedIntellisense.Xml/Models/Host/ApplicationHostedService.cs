using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VsLocalizedIntellisense.Xml.Models.Host
{
    public class ApplicationHostedService: IHostedService
    {
        public ApplicationHostedService(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        private IConfiguration Configuration { get; }
        private ILogger Logger { get; }

        #endregion

        #region function

        private static (string rawDirectory, string intellisenseDirectory, string dotnetVersionClr, string dotnetVersionStandard) GetRequiredOptions(IConfiguration configuration)
        {
            string GetConfiguration(string key)
            {
                var result = configuration.GetValue<string>(key);

                if(string.IsNullOrWhiteSpace(result)) {
                    throw new InvalidOperationException(key);
                }

                return result;
            }

            var rawDirectory = GetConfiguration("raw_dir");
            var intellisenseDirectory = GetConfiguration("intellisense_dir");
            var dotnetVersionClr = GetConfiguration("dotnet_version_clr");
            var dotnetVersionStandard = GetConfiguration("dotnet_version_standard");

            return (
                rawDirectory: rawDirectory,
                intellisenseDirectory: intellisenseDirectory,
                dotnetVersionClr: dotnetVersionClr,
                dotnetVersionStandard: dotnetVersionStandard
            );
        }

        #endregion

        #region IHostedService

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("hello world!");

            var options = GetRequiredOptions(Configuration);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
