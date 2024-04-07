using System;
using System.Runtime.CompilerServices;

namespace VsLocalizedIntellisense.Models.Logger
{
    public abstract class LoggerBase: ILogger
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
        /// </summary>
        /// <remarks>
        /// <para>ログレベル判定は行わない。</para>
        /// <para>他のロガーから呼ばれることを想定しているため<see langword="public"/>としているがユーザーコードで使用することは想定していない。<see cref="LoggerBase"/>自体ユーザーコードには出てこないからまぁなんでもいいけど。</para>
        /// </remarks>
        /// <param name="logItem"></param>
        public abstract void OutputLog(in LogItem logItem);

        #endregion

        #region ILogger

        public virtual bool IsEnabled(LogLevel logLevel)
        {
            return Logging.IsEnabled(CurrentLogLevel, logLevel);
        }

        public void Log(LogLevel logLevel, string logMessage, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            if(!IsEnabled(logLevel)) {
                return;
            }

            var logItem = new LogItem(DateTime.UtcNow, logLevel, logMessage, callerMemberName, callerFilePath, callerLineNumber);
            OutputLog(logItem);
        }


        #endregion
    }

    public abstract class LoggerBase<TLogOptions>: LoggerBase
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
