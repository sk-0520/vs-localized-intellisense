using System.Collections.Generic;
using System.Linq;
using System.Text;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value.Special;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Command
{
    public abstract class IfCommandBase: CommandBase
    {
        public IfCommandBase()
            : base(Name)
        { }

        #region property

        public static string Name { get; } = "if";
        public static string ElseName { get; } = "else";

        public bool IsNot { get; set; }

        protected abstract Express Condition { get; }

        public IList<CommandBase> TrueBlock { get; } = new List<CommandBase>();
        public IList<CommandBase> FalseBlock { get; } = new List<CommandBase>();

        #endregion

        #region CommandBase

        public override string GetStatement()
        {
            var sb = new StringBuilder();

            sb.Append(GetStatementCommandName());
            sb.Append(' ');
            if(IsNot) {
                sb.Append("not ");
            }
            sb.Append(Condition.Expression);
            sb.AppendLine(" (");
            foreach(var command in TrueBlock) {
                sb.Append(IndentSpace);
                sb.AppendLine(command.GetStatement());
            }
            if(FalseBlock.Any()) {
                sb.Append(") ");

                sb.Append(ElseName);
                sb.AppendLine(" (");
                foreach(var command in FalseBlock) {
                    sb.Append(IndentSpace);
                    sb.AppendLine(command.GetStatement());
                }
                sb.Append(")");
            } else {
                sb.Append(")");
            }

            return sb.ToString();
        }

        #endregion
    }

    public class IfErrorLevelCommand: IfCommandBase
    {
        #region property

        public int Level { get; set; }

        #endregion

        #region IfCommandBase

        protected override Express Condition
        {
            get
            {
                var result = new Express();
                result.Values.Add(new Text(ErrorLevel.Instance.Name));
                result.Values.Add(new Text(" "));
                result.Values.Add(new Text(Level.ToString()));

                return result;
            }
        }

        #endregion
    }

    // あ、これどうしよ
    public enum CompareOperation
    {
        Default,
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
    }

    public class IfExpressCommand: IfCommandBase
    {
        #region property

        public Express Left { get; set; }
        public Express Right { get; set; }

        public CompareOperation CompareOperation { get; set; }

        #endregion

        #region IfCommandBase

        protected override Express Condition
        {
            get
            {
                var result = new Express();

                result.Values.Add(new Text(Left.Escape()));
                result.Values.Add(new Text(" == "));
                result.Values.Add(new Text(Right.Escape()));

                return result;
            }
        }

        #endregion
    }

    public class IfExistCommand: IfCommandBase
    {
        #region property

        public Express Path { get; set; }

        #endregion

        #region IfCommandBase

        protected override Express Condition
        {
            get
            {
                var result = new Express();
                result.Values.Add(new Text("exist "));
                result.Values.Add(new Text(Path.Escape()));

                return result;
            }
        }

        #endregion
    }
}
