using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Diff.Models.Element;

namespace VsLocalizedIntellisense.Diff.ViewModels
{
    public class LanguageItemViewModel: SimpleModelViewModelBase<LanguageItemElement>
    {
        public LanguageItemViewModel(LanguageItemElement model)
            : base(model)
        { }

        #region property

        public CultureInfo CultureInfo => Model.CultureInfo;
        public string DisplayName => Model.CultureInfo.DisplayName;
        public string NativeName => Model.CultureInfo.NativeName;

        #endregion
    }
}
