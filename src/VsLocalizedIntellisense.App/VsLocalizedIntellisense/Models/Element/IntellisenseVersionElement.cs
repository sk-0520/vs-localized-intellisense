using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Models.Element
{
    public class IntellisenseVersionElement : ElementBase
    {
        public IntellisenseVersionElement(string name, Version version, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            Name = name;
            Version = version;
        }

        #region proeprty

        public string Name { get; }
        public Version Version { get; }

        public string DirectoryName => $"{Name}{Version}";

        #endregion

    }
}
