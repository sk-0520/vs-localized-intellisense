using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Element;

namespace VsLocalizedIntellisense.Models.Logger
{
    /// <summary>
    /// ログ周りの共通処理。
    /// </summary>
    public static class Logging
    {
        #region define

        private class DefaultLogFormatOptions: ILogFormatOptions
        {
            public string Format { get; set; } = "${TIMESTAMP:UTC} ${LEVEL} ${CATEGORY} ${MESSAGE} ${FILE}(${LINE})";
        }

        #endregion

        #region property

        private static ILogFormatOptions DefaultLogFormatOptionsInstance { get; } = new DefaultLogFormatOptions();

        /// <summary>
        /// <see cref="Instance"/> 実体。
        /// </summary>
        private static AppLoggerFactory AppLoggerFactory { get; set; }

        /// <summary>
        /// アプリケーション全体で保持する <see cref="ILoggerFactory"/> を取得。
        /// </summary>
        /// <remarks>
        /// <para><seealso cref="Initialize">初期化処理</seealso>が終わっていることが前提となる。</para>
        /// </remarks>
        internal static ILoggerFactory Instance
        {
            get
            {
                Debug.Assert(AppLoggerFactory != null);
                return AppLoggerFactory;
            }
        }

        #endregion

        #region function

        /// <summary>
        /// <see cref="Instance"/> を使用するための初期化処理。
        /// </summary>
        /// <remarks>
        /// <para>使い終わったら<seealso cref="Shutdown">片付ける</seealso>。</para>
        /// </remarks>
        /// <param name="configuration"></param>
        /// <returns>アプリケーション全体で使用可能な<see cref="ILoggerFactory"/></returns>
        internal static ILoggerFactory Initialize(AppConfiguration configuration, ObservableCollection<LogItemElement> stockLogItems = null)
        {
            AppLoggerFactory = new AppLoggerFactory(configuration, stockLogItems);
            return AppLoggerFactory;
        }

        /// <summary>
        /// 初期化済みリソースの破棄。
        /// </summary>
        /// <remarks>べつに何もしてないけど。</remarks>
        internal static void Shutdown()
        {
            AppLoggerFactory?.Dispose();
        }

        /// <summary>
        /// 指定したログレベルはデフォルトレベルで有効か。
        /// </summary>
        /// <param name="defaultLevel">デフォルトログレベル。</param>
        /// <param name="compareLevel">確認するログレベル。</param>
        /// <returns></returns>
        public static bool IsEnabled(LogLevel defaultLevel, LogLevel compareLevel)
        {
            return compareLevel != LogLevel.None && defaultLevel <= compareLevel;
        }

        private static string FormatFromFormatOptions(string category, in LogItem logItem, LogOptionsBase options, ILogFormatOptions formatOptions)
        {
            Debug.Assert(!string.IsNullOrEmpty(formatOptions.Format));

            var localTimestamp = logItem.Timestamp.ToLocalTime();

            var map = new Dictionary<string, string>() {
                ["TIMESTAMP:LOCAL"] = localTimestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fff"),
                ["TIMESTAMP:UTC"] = logItem.Timestamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fff"),
                ["LEVEL"] = logItem.Level.ToString(),
                ["CATEGORY"] = category,
                ["MESSAGE"] = logItem.Message,
                ["FILE"] = logItem.CallerFilePath,
                ["LINE"] = logItem.CallerLineNumber.ToString(),
            };

            var message = Strings.ReplaceFromDictionary(formatOptions.Format, map);
            return message;
        }

        private static string FormatDefault(string category, in LogItem logItem, LogOptionsBase options)
        {
            return FormatFromFormatOptions(category, logItem, options, DefaultLogFormatOptionsInstance);
        }

        public static string Format(string category, in LogItem logItem, LogOptionsBase options)
        {
            if(options is ILogFormatOptions formatOptions && !string.IsNullOrEmpty(formatOptions.Format)) {
                return FormatFromFormatOptions(category, logItem, options, formatOptions);
            }

            return FormatDefault(category, logItem, options);
        }

        #endregion
    }
}
