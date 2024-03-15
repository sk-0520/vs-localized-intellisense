using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VsLocalizedIntellisense.Models.Logger
{
    internal sealed class MultiLogger: DisposerBase, ILogger
    {
        public MultiLogger(string category, MultiLogOptions multiLogOptions)
        {
            var loggers = new List<LoggerBase>();

            foreach(var currentOptions in multiLogOptions.Options.Values) {
                switch(currentOptions) {
                    case DebugLogOptions options: {
                            var logger = new DebugLogger(category, options);
                            loggers.Add(logger);
                        }
                        break;

                    case FileLogOptions options: {
                            var logger = new FileLogger(category, options);
                            loggers.Add(logger);
                        }
                        break;

                    case StockLogOptions options: {
                            var logger = new StockLogger(category, options);
                            loggers.Add(logger);
                        }
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            Loggers = loggers;
        }

        #region property

        private IReadOnlyCollection<LoggerBase> Loggers { get; }

        #endregion

        #region ILogger

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log(LogLevel logLevel, string logMessage, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var logItem = new LogItem(DateTime.UtcNow, logLevel, logMessage, callerMemberName, callerFilePath, callerLineNumber);

            foreach(var logger in Loggers) {
                if(logger.IsEnabled(logLevel)) {
                    logger.OutputLog(logItem);
                }
            }
        }

        #endregion

        #region DisposerBase

        protected override void Dispose(bool disposing)
        {
            if(!IsDisposed) {
                if(disposing) {
                    foreach(var logger in Loggers) {
                        if(logger is IDisposable disposable) {
                            disposable.Dispose();
                        }
                    }
                }
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
