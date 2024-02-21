using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VsLocalizedIntellisense.Models.Logger
{
    public abstract class LoggerBase : ILogger
    {
        protected LoggerBase(string category, LogOptionsBase options)
        {
            Category = category;
            CurrentLogLevel = options.Level;
        }

        #region property

        protected string Category { get; }

        /// <summary>
        /// ロガーの対象ログレベル。
        /// </summary>
        protected LogLevel CurrentLogLevel { get; }

        #endregion

        #region function

        /// <summary>
        /// ログ出力実装。
        /// <para>ログレベル判定は行わない。</para>
        /// </summary>
        /// <param name="log"></param>
        protected internal abstract void OutputLog(in LogItem logItem);

        #endregion

        #region ILogger

        public bool IsEnabled(LogLevel logLevel)
        {
            return Logging.IsEnabled(CurrentLogLevel, logLevel);
        }

        public void Log(LogLevel logLevel, string logMessage, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var logItem = new LogItem(DateTime.UtcNow, logLevel, logMessage, callerMemberName, callerFilePath, callerLineNumber);
            OutputLog(logItem);
        }


        #endregion
    }

    public abstract class LoggerBase<TLogOptions> : LoggerBase
        where TLogOptions : LogOptionsBase
    {
        protected LoggerBase(string category, TLogOptions options)
            : base(category, options)
        {
            Options = options;
        }

        #region property

        protected TLogOptions Options { get; }

        #endregion
    }
}
