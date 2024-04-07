using System.Collections.Generic;
using System.Collections.ObjectModel;
using VsLocalizedIntellisense.Models.Element;

namespace VsLocalizedIntellisense.Models.Logger
{
    /// <summary>
    /// ログオプション。
    /// </summary>
    public abstract class LogOptionsBase
    {
        #region property

        /// <summary>
        /// デフォルトログレベル。
        /// </summary>
        /// <remarks>
        /// <para>このレベル未満はログ出力対象外となる。</para>
        /// </remarks>
        public LogLevel Level { get; set; }

        #endregion
    }

    /// <summary>
    /// ログ出力時のフォーマット指定。
    /// </summary>
    /// <remarks>
    /// <para>本インターフェイスを実装しない、もしくは空の場合、標準のフォーマットが使用される。</para>
    /// </remarks>
    public interface ILogFormatOptions
    {
        #region property

        /// <summary>
        /// 書式設定。
        /// </summary>
        string Format { get; set; }

        #endregion
    }

    /// <summary>
    /// <see cref="DebugLogger"/> で使用されるオプション。
    /// </summary>
    public class DebugLogOptions: LogOptionsBase, ILogFormatOptions
    {
        #region ILogFormatOptions

        public string Format { get; set; } = string.Empty;

        #endregion
    }

    /// <summary>
    /// <see cref="FileLogger"/> で使用されるオプション。
    /// </summary>
    public class FileLogOptions: LogOptionsBase, ILogFormatOptions
    {
        #region proeprty

        public string FilePath { get; set; }

        #endregion

        #region ILogFormatOptions

        public string Format { get; set; } = string.Empty;

        #endregion
    }

    public class StockLogOptions: LogOptionsBase
    {
        #region property

        public ILoggerFactory LoggerFactory { get; set; }
        public ObservableCollection<LogItemElement> LogItems { get; set; }

        #endregion
    }

    /// <summary>
    /// 複数出力ログオプション。
    /// </summary>
    /// <remarks>
    /// <para>明示的に使用することはない。</para>
    /// </remarks>
    public sealed class MultiLogOptions
    {
        #region property

        public IDictionary<string, LogOptionsBase> Options { get; set; } = new Dictionary<string, LogOptionsBase>();

        #endregion
    }
}
