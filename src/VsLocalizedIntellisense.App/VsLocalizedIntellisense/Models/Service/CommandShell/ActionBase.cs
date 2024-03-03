using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;
using VsLocalizedIntellisense.Models.Service.CommandShell.Redirect;

namespace VsLocalizedIntellisense.Models.Service.CommandShell
{
    /// <summary>
    /// 何かしらのコマンド。
    /// </summary>
    public abstract class ActionBase
    {
        #region property

        /// <summary>
        /// コマンド名。
        /// </summary>
        public virtual string CommandName { get; set; }
        /// <summary>
        /// 標準入力ファイル。
        /// </summary>
        public Express Input { get; set; }
        /// <summary>
        /// リダイレクト先。
        /// </summary>
        public OutputRedirect Redirect { get; set; }
        /// <summary>
        /// パイプ先。
        /// </summary>
        public ActionBase Pipe { get; set; }

        #endregion

        #region function

        /// <summary>
        /// コマンドの出力。
        /// </summary>
        /// <returns>インデントとかない状態。</returns>
        public abstract string GetStatement();

        /// <summary>
        /// 文脈に合わせたコマンドの出力。
        /// </summary>
        /// <param name="indent">インデント。</param>
        /// <returns>インデントされたあれこれ。</returns>
        public virtual string ToStatement(IndentContext indent)
        {
            var sb = new StringBuilder();

            var statement = GetStatement();
            sb.Append(statement);

            if (Input != null)
            {
                sb.Append(" < ");
                sb.Append(Input.Expression);
            }

            if (Pipe != null)
            {
                var pipeAction = Pipe.ToStatement(new IndentContext());
                sb.Append(" | ");
                sb.Append(pipeAction);
            }
            else if (Redirect != null)
            {
                var redirect = Redirect.Expression;
                if (!string.IsNullOrWhiteSpace(redirect))
                {
                    sb.Append(' ');
                    sb.Append(redirect);
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}
