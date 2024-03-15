namespace VsLocalizedIntellisense.Models.Mvvm.Message
{
    /// <summary>
    /// メッセージ内容。
    /// <para>登録時の型とIDがキーとなる。</para>
    /// <para>入力値・コールバックなどを必要に応じて実装していく。</para>
    /// </summary>
    public interface IMessage
    {
        #region property

        /// <summary>
        /// メッセージを特定するID。
        /// <para>メッセージは最初に見つかったものが使用されるため同じメッセージに対して処理を振り分けるために使用する。</para>
        /// </summary>
        string MessageId { get; }

        #endregion
    }
}
