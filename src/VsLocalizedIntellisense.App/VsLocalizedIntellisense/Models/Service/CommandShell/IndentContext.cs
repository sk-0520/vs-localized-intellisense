namespace VsLocalizedIntellisense.Models.Service.CommandShell
{
    /// <summary>
    /// 現在インデント。
    /// </summary>
    /// <remarks>
    /// <para>まぁきちんと動いてないけど使うこともないのでどうでもよさげ</para>
    /// </remarks>
    public class IndentContext
    {
        public IndentContext(string space = CommandShellHelper.IndentSpace, int level = 0)
        {
            Space = space;
            Level = level;
        }

        #region property

        /// <summary>
        /// インデントに使用する文字列。
        /// </summary>
        public string Space { get; }
        /// <summary>
        /// インデントレベル。
        /// </summary>
        /// <remarks>
        /// <para>0 が最上位。</para>
        /// </remarks>
        public int Level { get; }

        #endregion

        #region function

        /// <summary>
        /// ネスト。
        /// </summary>
        /// <returns>ネスト後のインデント。</returns>
        public IndentContext Nest()
        {
            return new IndentContext(Space, Level + 1);
        }

        #endregion
    }
}
