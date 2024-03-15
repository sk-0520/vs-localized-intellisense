using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Service.CommandShell
{
    public class CommandShellOptions
    {
        #region property

        public bool SuppressCommand { get; set; }
        public bool CommandNameIsUpper { get; set; }

        public string IndentSpace { get; set; } = CommandShellHelper.IndentSpace;

        public Encoding Encoding { get; set; } = Encoding.Default; // .NET Framework と心中

        public string NewLine { get; set; } = Environment.NewLine;

        #endregion
    }
}
