using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Data;
using VsLocalizedIntellisense.Models.Service.GitHub;

namespace VsLocalizedIntellisense.Models.Service.Application
{
    public static class GitHubServiceExtensions
    {
        #region function

        /// <summary>
        /// インテリセンスバージョン一覧を取得。
        /// <para>Repository/intellisense/*</para>
        /// </summary>
        /// <param name="service"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetIntellisenseVersionItemsAsync(this GitHubService service, string revision, CancellationToken cancellationToken = default)
        {
            var contentPath = "intellisense";
            var intellisenseItems = await service.GetContentsAsync(revision, contentPath, cancellationToken);
            return intellisenseItems.Select(a => a.Name);
        }

        /// <summary>
        /// 対象ライブラリのインテリセンスファイル一覧を取得。
        /// <para>Repository/intellisense/version/library-dir-name/*</para>
        /// </summary>
        /// <param name="service"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetIntellisenseLanguageItemsAsync(this GitHubService service, string revision, IntellisenseLanguageParts parts, CancellationToken cancellationToken = default)
        {
            var contentPath = service.JoinPath("intellisense", parts.IntellisenseVersion, parts.LibraryName, parts.Language);
            var intellisenseItems = await service.GetContentsAsync(revision, contentPath, cancellationToken);
            return intellisenseItems.Select(a => a.Name);
        }

        public static string BuildPath(this GitHubService service, IntellisenseLanguageParts parts, string fileName)
        {
            return service.JoinPath("intellisense", parts.IntellisenseVersion, parts.LibraryName, parts.Language, fileName);
        }

        #endregion
    }
}
