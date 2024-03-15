using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Service.GitHub;

namespace VsLocalizedIntellisense.Models.Service.Application
{
    public class AppGitHubService : GitHubService
    {
        public AppGitHubService(AppConfiguration configuration, ILoggerFactory loggerFactory)
            : base(
                 new GitHubRepository(configuration.GetRepositoryOwner(), configuration.GetRepositoryName()),
                 new GitHubOptions()
                 {
                     RequestHeaders = new Dictionary<string, string>()
                     {
                         ["User-Agent"] = configuration.GetHttpUserAgent(),
                         ["Accept"] = "application/vnd.github+json",
                         ["X-GitHub-Api-Version"] = "2022-11-28",
                     }
                 },
                 loggerFactory
            )
        { }
    }
}
