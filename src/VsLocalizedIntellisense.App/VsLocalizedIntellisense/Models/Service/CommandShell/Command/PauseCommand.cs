namespace VsLocalizedIntellisense.Models.Service.CommandShell.Command
{
    public class PauseCommand: CommandBase
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
