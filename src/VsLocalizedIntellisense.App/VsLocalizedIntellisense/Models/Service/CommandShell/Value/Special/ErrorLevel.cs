namespace VsLocalizedIntellisense.Models.Service.CommandShell.Value.Special
{
    public sealed class ErrorLevel: Variable
    {
        public ErrorLevel()
            : base("ERRORLEVEL", true)
        { }

        #region property

        public static ErrorLevel Instance => new ErrorLevel();

        #endregion
    }
}
