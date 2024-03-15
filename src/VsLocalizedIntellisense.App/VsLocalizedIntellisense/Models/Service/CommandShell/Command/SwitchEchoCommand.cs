using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Command
{
    public sealed class SwitchEchoCommand : CommandBase
    {
        public SwitchEchoCommand()
            : base(EchoCommand.Name)
        {
            SuppressCommand = true;
        }

        #region property

        public bool On { get; set; }

        #endregion

        #region Echo

        public override string GetStatement()
        {
            var value = On ? "on" : "off";
            return $"{GetStatementCommandName()} {value}";
        }

        #endregion
    }
}
