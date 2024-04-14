using System;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Models.Element
{
    public class LibraryVersionElement: ElementBase
    {
        public LibraryVersionElement(string rawVersion, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            RawVersion = rawVersion ?? throw new ArgumentNullException(nameof(rawVersion));
            Version = new Version(RawVersion);
        }

        #region proeprty

        private string RawVersion { get; }

        public Version Version { get; }

        #endregion
    }
}
