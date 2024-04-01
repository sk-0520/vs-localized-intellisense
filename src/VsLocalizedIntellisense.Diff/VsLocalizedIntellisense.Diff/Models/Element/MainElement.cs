using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Diff.Models.Element
{
    public class MainElement
    {
        public MainElement(IAppPath appPath)
        {
            AppPath = appPath;
        }

        #region property

        private IAppPath AppPath { get; }

        #endregion
    }
}
