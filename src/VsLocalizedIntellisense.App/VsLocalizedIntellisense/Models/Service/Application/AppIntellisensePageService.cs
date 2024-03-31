using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Models.Service.Application
{
    public class AppIntellisensePageService
    {
        public AppIntellisensePageService(AppConfiguration configuration, ILoggerFactory loggerFactory)
            : this(new HttpClient(), configuration, loggerFactory)
        { }

        public AppIntellisensePageService(HttpClient httpClient, AppConfiguration configuration, ILoggerFactory loggerFactory)
        {
            HttpClient = httpClient;
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        private HttpClient HttpClient { get; }
        private AppConfiguration Configuration { get; }
        private ILogger Logger { get; }

        #endregion

        #region function

        private HttpRequestMessage CreateRequestMessage(HttpMethod method, Uri uri)
        {
            var request = new HttpRequestMessage(method, uri);

            var httpUserAgent = Configuration.GetHttpUserAgent();

            request.Headers.Add("User-Agent", httpUserAgent);

            return request;
        }

        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string uri)
            => CreateRequestMessage(method, new Uri(uri));

        private async Task<Stream> RequestStreamAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if(Logger.IsEnabled(LogLevel.Debug)) {
                foreach(var header in response.Headers) {
                    Logger.LogTrace($"{header.Key}: {string.Join(", ", header.Value)}");
                }
            }
            return await response.Content.ReadAsStreamAsync();
        }

        private async Task<T> RequestJsonAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using(var stream = await RequestStreamAsync(request, cancellationToken)) {
                var serializer = new DataContractJsonSerializer(typeof(T));
                var rawContent = serializer.ReadObject(stream);
                return (T)rawContent;
            }
        }

        public async Task<IntellisensePageList> GetDataListAsync(string path, CancellationToken cancellationToken = default)
        {
            var pageBaseUrl = Configuration.GetValue<string>("page-base-url");

            var url = Strings.ReplaceFromDictionary(
                "${BASE_URL}/data/${PATH}/list.json",
                new Dictionary<string, string>() {
                    ["BASE_URL"] = pageBaseUrl,
                    ["PATH"] = path.Trim('/'),
                }
            );
            var request = CreateRequestMessage(HttpMethod.Get, url);
            var response = await RequestJsonAsync<IntellisensePageList>(request, cancellationToken);
            if(response.Files == null) {
                response.Files = Array.Empty<string>();
            }
            if(response.Directories == null) {
                response.Directories = Array.Empty<string>();
            }

            return response;
        }

        public async Task<Stream> GetDataStreamAsync(string path, CancellationToken cancellationToken = default)
        {
            var pageBaseUrl = Configuration.GetValue<string>("page-base-url");

            var url = Strings.ReplaceFromDictionary(
                "${BASE_URL}/data/${PATH}",
                new Dictionary<string, string>() {
                    ["BASE_URL"] = pageBaseUrl,
                    ["PATH"] = path,
                }
            );
            Logger.LogInformation($"URL: {url}");
            var request = CreateRequestMessage(HttpMethod.Get, url);
            var stream = await RequestStreamAsync(request, cancellationToken);
            return stream;

        }

        #endregion
    }
}
