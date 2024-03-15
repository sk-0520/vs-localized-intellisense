using System;

namespace VsLocalizedIntellisense.Models.Logger
{
    public interface ILoggerFactory: IDisposable
    {
        #region function

        ILogger CreateLogger(string categoryName);

        #endregion
    }
}
