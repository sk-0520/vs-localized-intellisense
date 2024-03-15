using System;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;

namespace VsLocalizedIntellisense.ViewModels
{
    public class LibraryVersionViewModel: SingleModelViewModelBase<LibraryVersionElement>
    {
        public LibraryVersionViewModel(LibraryVersionElement model, ILoggerFactory loggerFactory)
            : base(model, loggerFactory)
        { }

        #region property

        public Version Version => Model.Version;

        #endregion
    }
}
