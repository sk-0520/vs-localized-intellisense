namespace VsLocalizedIntellisense.Models.Data
{
    public class IntellisenseLanguageParts
    {
        #region property

        public string IntellisenseVersion { get; set; }
        public string LibraryName { get; set; }
        public string Language { get; set; }

        #endregion

        #region object

        public override string ToString()
        {
            return $"{IntellisenseVersion}/{LibraryName}/{Language}";
        }
        #endregion
    }
}
