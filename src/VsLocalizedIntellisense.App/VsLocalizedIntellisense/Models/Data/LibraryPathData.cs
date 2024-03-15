using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Data
{
    public class LibraryPathData
    {
        public LibraryPathData(string dotNetVersion, string libraryName, string language)
        {
            DotNetVersion = dotNetVersion;
            LibraryName = libraryName;
            Language = language;
        }

        #region property

        public string DotNetVersion { get; }
        public string LibraryName { get; }
        public string Language { get; }

        #endregion
    }
}
