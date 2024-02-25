using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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
        Trace,
        /// <summary>
        /// 開発時に対話型調査に使用されるログ。 これらのログには、主にデバッグに役立つ情報が含まれており、長期的な値は含まれていません。
        /// </summary>
        Debug,
        /// <summary>
        /// アプリケーションの一般的なフローを追跡するログ。 これらのログには長期的な値を含める必要があります。
        /// </summary>
        Information,
        /// <summary>
        /// アプリケーション フロー内の異常なイベントまたは予期しないイベントを強調表示し、それ以外の場合はアプリケーションの実行を停止するログ。
        /// </summary>
        Warning,
        /// <summary>
        /// エラーによって現在の実行フローが停止したときに強調表示されるログ。 これらは、アプリケーション全体の障害ではなく、現在のアクティビティの失敗を示す必要があります。
        /// </summary>
        Error,
        /// <summary>
        /// 回復不可能なアプリケーションまたはシステムのクラッシュや、早急に対処する必要がある重大な障害について説明するログ。
        /// </summary>
        Critical,
        /// <summary>
        /// ログ メッセージの記述には使用されません。 ログのカテゴリにメッセージを記述しないように指定します。
        /// </summary>
        None,
    }
}
