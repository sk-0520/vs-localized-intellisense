using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Command
{
    public class PauseCommand : CommandBase
    {
        public PauseCommand()
            : base(Name)
        { }

        #region property

        public static string Name { get; } = "pause";

        #endregion

        #region CommandBase

        public override string GetStatement()
        {
            return $"{GetStatementCommandName()}";
        }

        #endregion
    }
}
