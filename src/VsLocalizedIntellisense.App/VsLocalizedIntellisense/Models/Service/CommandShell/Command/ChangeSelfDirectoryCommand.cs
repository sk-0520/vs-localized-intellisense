using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Command
{
    public sealed class ChangeSelfDirectoryCommand : CommandBase
    {
        public ChangeSelfDirectoryCommand()
            : base(ChangeDirectoryCommand.Name)
        { }

        #region CommandBase

        public override string GetStatement()
        {
            return $"{GetStatementCommandName()} /d %~dp0";
        }

        #endregion
    }
}
