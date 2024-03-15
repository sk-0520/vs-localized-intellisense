using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VsLocalizedIntellisense.Raw.Models.Host
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

        #region IHostedService

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("hello world!");

            var librariesSection = Configuration.GetSection("libraries");
            var libraries = librariesSection.Get<string[]>() ?? throw new ApplicationException("libraries");
            foreach(var library in libraries) {
                Logger.LogInformation("{library}", library);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
