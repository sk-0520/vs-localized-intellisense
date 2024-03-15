using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Element;

namespace VsLocalizedIntellisense.Models.Logger
{
    public class StockLogger : LoggerBase<StockLogOptions>
    {
        public StockLogger(string category, StockLogOptions options)
            : base(category, options)
        { }

        #region ILogger

        public override bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public override void OutputLog(in LogItem logItem)
        {
            Options.LogItems.Add(new LogItemElement(logItem, Options.LoggerFactory));
        }

        #endregion
    }

}
