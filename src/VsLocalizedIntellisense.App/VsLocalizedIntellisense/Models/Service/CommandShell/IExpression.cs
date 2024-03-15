namespace VsLocalizedIntellisense.Models.Service.CommandShell
{
    /// <summary>
    /// テキスト表現。
    /// </summary>
    public interface IExpression
    {
        #region property

        /// <summary>
        /// テキストを取得。
        /// </summary>
        string Expression { get; }

        #endregion
    }
}
