using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Service.CommandShell
{
    public sealed class EmptyLine : ActionBase
    {
        public override string GetStatement()
        {
            return string.Empty;
        }
    }
}
