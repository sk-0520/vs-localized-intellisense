using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public ApplicationHostedService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            HttpClientFactory = httpClientFactory;
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        private IHttpClientFactory HttpClientFactory { get; }
        private IConfiguration Configuration { get; }
        private ILogger Logger { get; }

        #endregion

        #region IHostedService

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("hello world!");

            var installBaseDirectoryPath = Configuration.GetValue<string>("install_base_dir");
            if(string.IsNullOrWhiteSpace(installBaseDirectoryPath)) {
                throw new InvalidOperationException("install_base_dir");
            }

            var httpClient = HttpClientFactory.CreateClient();

            var nugetBaseUrl = Configuration.GetValue<string>("nuget_base_url");
            if(string.IsNullOrWhiteSpace(nugetBaseUrl)) {
                throw new InvalidOperationException("nuget_base_url");
            }
            var nugetDownloadPath = Configuration.GetValue<string>("nuget_download_path");
            if(string.IsNullOrWhiteSpace(nugetDownloadPath)) {
                throw new InvalidOperationException("nuget_download_path");
            }
            var librariesSection = Configuration.GetSection("libraries");
            var libraries = librariesSection.Get<string[]>() ?? throw new ApplicationException("libraries");
            foreach(var library in libraries) {
                var uri = UrlHelper.JoinUri(nugetBaseUrl, nugetDownloadPath, library);
                Logger.LogInformation("{library}: {uri}", library, uri);
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
