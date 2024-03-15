using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Command
{
    public class MakeDirectoryCommand: CommandBase
    {
        public MakeDirectoryCommand()
            : base(Name)
        { }

        #region property

        public static string Name { get; } = "mkdir";
        public Express Path { get; set; }

        #endregion

        #region CommandBase

        public override string GetStatement()
        {
            return $"{GetStatementCommandName()} {Path.Escape()}";
        }

        #endregion
    }
}
