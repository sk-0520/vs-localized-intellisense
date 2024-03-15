using System;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;

namespace VsLocalizedIntellisense.ViewModels
{
    public class LogItemViewModel: SingleModelViewModelBase<LogItemElement>
    {
        public LogItemViewModel(LogItemElement model, ILoggerFactory loggerFactory)
            : base(model, loggerFactory)
        { }

        #region function

        public DateTime LocalTimestamp => Model.LogItem.Timestamp.ToLocalTime();

        public LogLevel Level => Model.LogItem.Level;

        public string Message => Model.LogItem.Message;


        #endregion
    }
}
