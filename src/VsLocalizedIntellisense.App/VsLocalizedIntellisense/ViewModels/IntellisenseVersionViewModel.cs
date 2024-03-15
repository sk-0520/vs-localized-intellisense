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
    public class IntellisenseVersionViewModel : SingleModelViewModelBase<IntellisenseVersionElement>
    {
        public IntellisenseVersionViewModel(IntellisenseVersionElement model, ILoggerFactory loggerFactory)
            : base(model, loggerFactory)
        { }

        #region property

        public Version Version => Model.Version;

        #endregion
    }
}
