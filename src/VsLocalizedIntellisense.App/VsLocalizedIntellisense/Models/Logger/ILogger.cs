using System.Runtime.CompilerServices;

namespace VsLocalizedIntellisense.Models.Logger
{
    public interface ILogger
    {
        #region function

        bool IsEnabled(LogLevel logLevel);

        void Log(LogLevel logLevel, string logMessage, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

        #endregion
    }
}
