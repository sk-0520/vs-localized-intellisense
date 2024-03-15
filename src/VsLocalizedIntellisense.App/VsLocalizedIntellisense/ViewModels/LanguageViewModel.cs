using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;

namespace VsLocalizedIntellisense.ViewModels
{
    public class LanguageViewModel : SingleModelViewModelBase<LanguageElement>
    {
        public LanguageViewModel(LanguageElement model, ILoggerFactory loggerFactory)
            : base(model, loggerFactory)
        { }

        #region property

        public string Language => Model.Language;

        #endregion
    }
}
