using System.Runtime.CompilerServices;

namespace VsLocalizedIntellisense.Models.Logger
{
    public sealed class NullLoggerFactory: ILoggerFactory
    {
        #region property

        public static ILoggerFactory Instance { get; } = new NullLoggerFactory();

        #endregion

        #region ILoggerFactory

        public ILogger CreateLogger(string categoryName)
        {
            return new NullLogger(categoryName);
        }

        public void Dispose()
        { }

        #endregion
    }

    public sealed class NullLogger: ILogger
    {
        public NullLogger()
            : this(string.Empty)
        { }

        public NullLogger(string categoryName)
        {
            CategoryName = categoryName;
        }

        #region property

        public string CategoryName { get; }

        public static ILogger Instance { get; } = new NullLogger(string.Empty);

        #endregion

        #region ILogger

        public bool IsEnabled(LogLevel logLevel) => false;

        public void Log(LogLevel logLevel, string logMessage, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        { }

        #endregion
    }
}
