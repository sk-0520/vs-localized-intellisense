using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Command
{
    // /p はサポートしない: するなら別処理
    public class SetVariableCommand : CommandBase
    {
        public SetVariableCommand()
            : base(Name)
        { }

        #region property

        public static string Name { get; } = "set";

        public string VariableName { get; set; }

        public Express Value { get; set; } = string.Empty;

        public bool IsExpress { get; set; }

        public Variable Variable
        {
            get
            {
                var variable = new Variable(VariableName);
                return variable;
            }
        }

        #endregion

        #region CommandBase

        public override string GetStatement()
        {
            if (string.IsNullOrWhiteSpace(VariableName))
            {
                throw new InvalidOperationException(nameof(VariableName));
            }

            var sb = new StringBuilder();

            sb.Append(GetStatementCommandName());
            sb.Append(' ');

            if (IsExpress)
            {
                sb.Append("/a ");
            }

            sb.Append(Variable.Name);
            sb.Append('=');
            sb.Append(Value.Expression);

            return sb.ToString();
        }

        #endregion
    }
}
