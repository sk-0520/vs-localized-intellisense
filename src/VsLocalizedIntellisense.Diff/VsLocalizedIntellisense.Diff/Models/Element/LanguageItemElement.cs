using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VsLocalizedIntellisense.Diff.Models.Element
{
    public class LanguageItemElement: ObservableObject
    {
        public LanguageItemElement(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
        }

        #region property

        public CultureInfo CultureInfo { get; }

        #endregion
    }
}
