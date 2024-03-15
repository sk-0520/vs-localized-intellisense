using System.Diagnostics;

namespace VsLocalizedIntellisense.Models.Logger
{
    public sealed class DebugLogger: LoggerBase<DebugLogOptions>
    {
        public DebugLogger(string category, DebugLogOptions options)
            : base(category, options)
        { }

        #region OutputLoggerBase

        public override void OutputLog(in LogItem logItem)
        {
            var log = Logging.Format(Category, logItem, Options);
            Debug.WriteLine(log);
        }

        #endregion
    }
}
