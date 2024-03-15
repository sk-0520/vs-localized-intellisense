using System.Text;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Redirect
{
    /// <summary>
    /// リダイレクト基底。
    /// </summary>
    public abstract class RedirectBase : IExpression
    {
        #region property

        /// <summary>
        /// リダイレクト先。
        /// </summary>
        public Express Target { get; set; }
        /// <summary>
        /// 追記するか。
        /// </summary>
        public bool Append { get; set; }

        #endregion

        #region IExpression

        public virtual string Expression
        {
            get
            {
                if (Target == null)
                {
                    return string.Empty;
                }

                var sb = new StringBuilder();

                var redirectMode = Append
                    ? ">>"
                    : ">"
                ;
                sb.Append(redirectMode);
                sb.Append(' ');
                sb.Append(Target.Expression);

                return sb.ToString();
            }
        }

        #endregion
    }
}
