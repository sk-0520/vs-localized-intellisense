namespace VsLocalizedIntellisense.Models.Service.CommandShell
{
    public sealed class EmptyLine: ActionBase
    {
        public override string GetStatement()
        {
            return string.Empty;
        }
    }
}
