using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

        #region function

        private async Task DownloadLibraryAsync(Uri uri, string installDirectoryPath, HttpClient httpClient)
        {
            using var stream = await httpClient.GetStreamAsync(uri);
            using var archive = new ZipArchive(stream);

            var refItems = archive.Entries
                // くっそ適当
                .Where(a => a.FullName.Contains("ref") && Path.GetExtension(a.Name) == ".xml")
                .ToArray()
            ;

            foreach(var refItem in refItems) {
                var installFilePath = Path.Combine(installDirectoryPath, refItem.Name);
                using var entryStream = refItem.Open();
                using var saveStream = new FileStream(installFilePath, FileMode.Create);
                await entryStream.CopyToAsync(saveStream);
            }
        }

        #endregion

        #region IHostedService

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("hello world!");

            var installBaseDirectoryPath = Configuration.GetValue<string>("install_base_dir");
            if(string.IsNullOrWhiteSpace(installBaseDirectoryPath)) {
                throw new InvalidOperationException("install_base_dir");
            }

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
            var libraryMapping = Configuration.GetSection("mapping").GetChildren()
                  .ToDictionary(k => k.Key, v => v.Value)
            ;
            if(libraries.Length != libraryMapping.Count) {
                throw new ApplicationException("libraries.Length != libraryMapping.Count");
            }

            var httpClient = HttpClientFactory.CreateClient();

            foreach(var library in libraries) {
                var installDirectoryPath = Path.Combine(installBaseDirectoryPath, libraryMapping[library] ?? throw new ArgumentException(library));
                if(!Directory.Exists(installDirectoryPath)) {
                    Directory.CreateDirectory(installDirectoryPath);
                }
                var uri = UrlHelper.JoinUri(nugetBaseUrl, nugetDownloadPath, library);
                Logger.LogInformation("{library}: {installDirectoryPath} {uri}", library, installDirectoryPath, uri);
                await DownloadLibraryAsync(uri, installDirectoryPath, httpClient);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
