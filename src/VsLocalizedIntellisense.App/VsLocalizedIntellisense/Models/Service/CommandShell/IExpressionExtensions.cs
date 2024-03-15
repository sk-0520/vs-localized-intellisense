using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Service.CommandShell
{
    public static class IExpressionExtensions
    {
        #region function

        public static string Escape(this IExpression expression)
        {
            return CommandShellHelper.Escape(expression.Expression);
        }

        #endregion
    }
}
