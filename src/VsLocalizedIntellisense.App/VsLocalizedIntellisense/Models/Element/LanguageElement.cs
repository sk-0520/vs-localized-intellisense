using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Models.Element
{
    public class LanguageElement: ElementBase
    {
        public LanguageElement(string language, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            Language = language;
        }

        #region property

        public string Language { get; }

        #endregion
    }
}
