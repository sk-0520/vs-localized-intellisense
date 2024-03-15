namespace VsLocalizedIntellisense.ViewModels
{
    public class ValidateMessage
    {
        public ValidateMessage(string message)
        {
            Message = message;
        }

        #region property

        public string Message { get; }

        #endregion

        #region object

        public override string ToString() => Message;

        #endregion
    }
}
