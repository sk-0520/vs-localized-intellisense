namespace VsLocalizedIntellisense.Models.Service.CommandShell.Value
{
    public class Text : ValueBase
    {
        public Text(string data)
        {
            Data = data;
        }

        #region property

        public string Data { get; set; }

        #endregion

        #region ValueBase

        public override string Expression => Data;

        #endregion
    }
}
