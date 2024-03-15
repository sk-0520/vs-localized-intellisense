using System.Text;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Redirect
{
    /// <summary>
    /// 標準出力リダイレクト。
    /// </summary>
    public class OutputRedirect : RedirectBase
    {
        #region property

        /// <summary>
        /// 空リダイレクト。
        /// </summary>
        public static OutputRedirect Null => new OutputRedirect()
        {
            Target = "NUL",
        };

        /// <summary>
        /// 空リダイレクト(エラー付き)。
        /// </summary>
        public static OutputRedirect NullWithError => new OutputRedirect()
        {
            Target = "NUL",
            Error = new ErrorRedirect()
            {
                StandardOutput = true,
            }
        };

        /// <summary>
        /// エラー出力先。
        /// </summary>
        public ErrorRedirect Error { get; set; }

        #endregion

        #region IExpression

        public override string Expression
        {
            get
            {
                var sb = new StringBuilder();

                var expression = base.Expression;
                var hasStdOut = !string.IsNullOrWhiteSpace(expression);
                if (hasStdOut)
                {
                    sb.Append(expression);
                }

                if (Error != null)
                {
                    if (Error.StandardOutput)
                    {
                        if (hasStdOut)
                        {
                            sb.Append(' ');
                        }
                        sb.Append("2>&1");
                    }
                    else
                    {
                        if (hasStdOut)
                        {
                            sb.Append(' ');
                        }

                        var redirect = Error.Expression;
                        if (!string.IsNullOrWhiteSpace(redirect))
                        {
                            sb.Append('2');
                            sb.Append(redirect);
                        }
                    }
                }

                return sb.ToString();
            }
        }

        #endregion
    }
}
