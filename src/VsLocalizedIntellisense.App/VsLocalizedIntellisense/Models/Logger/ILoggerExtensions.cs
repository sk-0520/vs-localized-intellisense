using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Logger
{
    public static class ILoggerExtensions
    {
        #region function

        /// <inheritdoc cref="LogLevel.Trace"/>
        public static void LogTrace(this ILogger logger, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            logger.Log(LogLevel.Trace, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        /// <inheritdoc cref="LogLevel.Debug"/>
        public static void LogDebug(this ILogger logger, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            logger.Log(LogLevel.Debug, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        /// <inheritdoc cref="LogLevel.Information"/>
        public static void LogInformation(this ILogger logger, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            logger.Log(LogLevel.Information, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        /// <inheritdoc cref="LogLevel.Warning"/>
        public static void LogWarning(this ILogger logger, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            logger.Log(LogLevel.Warning, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        /// <inheritdoc cref="LogLevel.Error"/>
        public static void LogError(this ILogger logger, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            logger.Log(LogLevel.Error, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        /// <inheritdoc cref="LogLevel.Critical"/>
        public static void LogCritical(this ILogger logger, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            logger.Log(LogLevel.Critical, message, callerMemberName, callerFilePath, callerLineNumber);
        }

        #endregion
    }
}
