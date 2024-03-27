using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Xml.Models
{
    public class IntellisenseItem
    {
        #region property

        public required string LibraryName { get; init; }
        public required DirectoryInfo RawDirectory { get; init; }

        public required FileInfo[] RawFiles { get; init; }

        public Dictionary<string, FileInfo[]> LanguageFiles { get; } = new Dictionary<string, FileInfo[]>();

        #endregion
    }
}
