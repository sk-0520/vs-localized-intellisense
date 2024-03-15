using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Service.CommandShell
{
    /// <summary>
    /// 現在インデント。
    /// <para>まぁきちんと動いてないけど使うこともないのでどうでもよさげ</para>
    /// </summary>
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
        /// <para>0 が最上位。</para>
        /// </summary>
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
