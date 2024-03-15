using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Models.Element
{
    public class LogItemElement: ElementBase
    {
        public LogItemElement(in LogItem logItem, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            LogItem = logItem;
        }

        #region property

        public LogItem LogItem { get; }

        #endregion
    }
}
