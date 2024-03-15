using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Json;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Models.Service.GitHub
{
    public class GitHubService
    {
        public GitHubService(GitHubRepository repository, GitHubOptions options, ILoggerFactory loggerFactory)
            : this(repository, options, new HttpClient(), loggerFactory)
        { }

        public GitHubService(GitHubRepository repository, GitHubOptions options, HttpClient httpClient, ILoggerFactory loggerFactory)
        {
            Repository = repository;
            Options = options;
            HttpClient = httpClient;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region proeprty
        protected ILogger Logger { get; }
        private HttpClient HttpClient { get; }
        private GitHubRepository Repository { get; }
        private GitHubOptions Options { get; }

        #endregion

        #region function

        //"https://api.github.com/repos/sk-0520/vs-localized-intellisense/contents/intellisense"

        public string JoinPath(string path1, string path2, params string[] pathN)
        {
            var items = new List<string>(2 + pathN.Length)
            {
                path1,
                path2
            };
            items.AddRange(pathN);

            var paths = items
                .Select(a => a.Trim('/', '\\'))
                .Where(a => !string.IsNullOrWhiteSpace(a))
                ;
            return string.Join("/", paths);
        }

        private HttpRequestMessage CreateRequestMessage(HttpMethod method, Uri uri)
        {
            var request = new HttpRequestMessage(method, uri);

            foreach (var pair in Options.RequestHeaders)
            {
                request.Headers.Add(pair.Key, pair.Value);
            }

            return request;
        }

        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string uri)
            => CreateRequestMessage(method, new Uri(uri));

        private async Task<Stream> RequestStreamAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                foreach (var header in response.Headers)
                {
                    Logger.LogTrace($"{header.Key}: {string.Join(", ", header.Value)}");
                }
            }
            return await response.Content.ReadAsStreamAsync();
        }

        private async Task<T> RequestJsonAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var stream = await RequestStreamAsync(request, cancellationToken))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                var rawContent = serializer.ReadObject(stream);
                return (T)rawContent;
            }
        }

        public async Task<IReadOnlyList<GitHubContentResponseItem>> GetContentsAsync(string revision, string path, CancellationToken cancellationToken = default)
        {
            var url = Strings.ReplaceFromDictionary(
                "https://api.github.com/repos/${OWNER}/${NAME}/contents/${PATH}?ref=${REVISION}",
                new Dictionary<string, string>()
                {
                    ["OWNER"] = Repository.Owner,
                    ["NAME"] = Repository.Name,
                    ["REVISION"] = revision,
                    ["PATH"] = path.Trim('/'),
                }
            );
            var request = CreateRequestMessage(HttpMethod.Get, url);
            var response = await RequestJsonAsync<GitHubContentResponseItem[]>(request, cancellationToken);
            return response;
        }

        // https://raw.githubusercontent.com/sk-0520/vs-localized-intellisense/master/intellisense/net7.0/Microsoft.NETCore.App.Ref/es/Microsoft.VisualBasic.Core.xml

        public async Task<Stream> GetRawAsync(string revision, string path, CancellationToken cancellationToken = default)
        {
            var url = Strings.ReplaceFromDictionary(
                "https://raw.githubusercontent.com/${OWNER}/${NAME}/${REVISION}/${PATH}",
                new Dictionary<string, string>()
                {
                    ["OWNER"] = Repository.Owner,
                    ["NAME"] = Repository.Name,
                    ["REVISION"] = revision,
                    ["PATH"] = path.Trim('/'),
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
