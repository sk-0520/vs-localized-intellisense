using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Raw.Models
{
    public class NuGetLibrary
    {
        #region function

        /// <summary>
        /// dll に対して同一階層同名の xml があれば XML ドキュメントとして取得する。
        /// <para>サテライトアセンブリがあるようなライブラリは本アプリの対象外なので多分これでいいはず。</para>
        /// </summary>
        /// <param name="archive"></param>
        /// <returns></returns>
        public IEnumerable<ZipArchiveEntry> GetXmlDocumentEntries(ZipArchive archive)
        {
            var assemblyEntries = archive.Entries
                .Where(a => Path.GetExtension(a.Name) == ".dll")
            ;

            //TODO: assemblyEntries に対して本当にアセンブリかどうかを確認する
            // https://learn.microsoft.com/ja-jp/dotnet/standard/assembly/identify
            // そんな大事なもんじゃないと思うのでいらんかなぁ

            var langEntries = assemblyEntries
                .Select(a => archive.GetEntry(Path.ChangeExtension(a.FullName, "xml")))
                .Where(a => a is not null)
                .Select(a => a!) // not null
            ;

            return langEntries;
        }

        #endregion
    }
}
