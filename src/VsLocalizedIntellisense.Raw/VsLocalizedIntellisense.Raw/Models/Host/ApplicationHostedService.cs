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

            // dll に対して同一階層同名の xml があれば該当のお目当てのものとする
            // サテライトアセンブリがあるようなライブラリは本アプリの対象外なので多分これでいいはず
            var assemblyEntries = archive.Entries
                .Where(a => Path.GetExtension(a.Name) == ".dll")
                .ToArray()
            ;

            //TODO: assemblyEntries に対して本当にアセンブリかどうかを確認する
            // https://learn.microsoft.com/ja-jp/dotnet/standard/assembly/identify
            // そんな大事なもんじゃないと思うのでいらんかなぁ

            var langEntries = assemblyEntries
                .Select(a => archive.GetEntry(Path.ChangeExtension(a.FullName, "xml")))
                .Where(a => a is not null)
                .ToArray()
            ;

            foreach(var langEntry in langEntries) {
                var installFilePath = Path.Combine(installDirectoryPath, langEntry!.Name);
                using var entryStream = langEntry.Open();
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

            var libraries = Configuration.GetSection("libraries").Get<string[]>() ?? throw new ApplicationException("libraries");
            var libraryMapping = Configuration.GetSection("mapping").Get<Dictionary<string, string>>() ?? throw new ApplicationException("mapping");
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
