using System.ComponentModel.DataAnnotations;
using VsLocalizedIntellisense.Properties;

namespace VsLocalizedIntellisense.Models.Logger
{
    /// <summary>
    /// ログの重大度レベルを定義します。
    /// </summary>
    /// <see href="https://learn.microsoft.com/ja-jp/dotnet/api/microsoft.extensions.logging.loglevel?view=dotnet-plat-ext-8.0"/>
    public enum LogLevel
    {
        /// <summary>
        /// 最も詳細なメッセージを含むログ。 これらのメッセージには、機密性の高いアプリケーション データが含まれる場合があります。 これらのメッセージは既定で無効になっているため、運用環境では有効にしないでください。
        /// </summary>
        [Display(Description = "enum_" + nameof(LogLevel) + "_" + nameof(Trace), ResourceType = typeof(Resources))]
        Trace,
        /// <summary>
        /// 開発時に対話型調査に使用されるログ。 これらのログには、主にデバッグに役立つ情報が含まれており、長期的な値は含まれていません。
        /// </summary>
        [Display(Description = "enum_" + nameof(LogLevel) + "_" + nameof(Debug), ResourceType = typeof(Resources))]
        Debug,
        /// <summary>
        /// アプリケーションの一般的なフローを追跡するログ。 これらのログには長期的な値を含める必要があります。
        /// </summary>
        [Display(Description = "enum_" + nameof(LogLevel) + "_" + nameof(Information), ResourceType = typeof(Resources))]
        Information,
        /// <summary>
        /// アプリケーション フロー内の異常なイベントまたは予期しないイベントを強調表示し、それ以外の場合はアプリケーションの実行を停止するログ。
        /// </summary>
        [Display(Description = "enum_" + nameof(LogLevel) + "_" + nameof(Warning), ResourceType = typeof(Resources))]
        Warning,
        /// <summary>
        /// エラーによって現在の実行フローが停止したときに強調表示されるログ。 これらは、アプリケーション全体の障害ではなく、現在のアクティビティの失敗を示す必要があります。
        /// </summary>
        [Display(Description = "enum_" + nameof(LogLevel) + "_" + nameof(Error), ResourceType = typeof(Resources))]
        Error,
        /// <summary>
        /// 回復不可能なアプリケーションまたはシステムのクラッシュや、早急に対処する必要がある重大な障害について説明するログ。
        /// </summary>
        [Display(Description = "enum_" + nameof(LogLevel) + "_" + nameof(Critical), ResourceType = typeof(Resources))]
        Critical,
        /// <summary>
        /// ログ メッセージの記述には使用されません。 ログのカテゴリにメッセージを記述しないように指定します。
        /// </summary>
        None,
    }
}
