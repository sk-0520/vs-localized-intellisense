using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Command
{
    public class ChangeCodePageCommand : CommandBase
    {
        public ChangeCodePageCommand()
            : base(Name)
        { }

        #region property

        public static string Name { get; } = "chcp";

        public Encoding Encoding { get; set; }

        #endregion

        #region function

        public override string GetStatement()
        {
            var encoding = Encoding ?? Encoding.Default;
            return $"{GetStatementCommandName()} {encoding.CodePage}";
        }

        #endregion
    }
}
