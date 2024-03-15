using System;

namespace VsLocalizedIntellisense.Models.Logger
{
    public readonly struct LogItem
    {
        public LogItem(DateTime timestamp, LogLevel level, string message, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            Timestamp = timestamp;
            Level = level;
            Message = message;
            CallerMemberName = callerMemberName;
            CallerFilePath = callerFilePath;
            CallerLineNumber = callerLineNumber;
        }

        #region property

        /// <summary>
        /// タイムスタンプ(UTC)。
        /// </summary>
        public DateTime Timestamp { get; }
        public LogLevel Level { get; }
        public string Message { get; }

        public string CallerMemberName { get; }
        public string CallerFilePath { get; }
        public int CallerLineNumber { get; }

        #endregion
    }
}
