using System.Collections.Generic;
using System.Linq;

namespace VsLocalizedIntellisense.Models.Service.CommandShell.Value
{
    public class Express: ValueBase
    {
        #region proeprty

        public IList<ValueBase> Values { get; } = new List<ValueBase>();

        #endregion

        #region operator

        public static implicit operator Express(string text)
        {
            var result = new Express();
            result.Values.Add(new Text(text));

            return result;
        }

        #endregion

        #region ValueBase

        public override string Expression
        {
            get
            {
                return string.Join(string.Empty, Values.Select(a => a.Expression));
            }
        }

        #endregion
    }
}
