using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Diff.Models;
using VsLocalizedIntellisense.Diff.Models.Binding;
using VsLocalizedIntellisense.Diff.Models.Element;

namespace VsLocalizedIntellisense.Diff.ViewModels
{
    public class LanguageSelectViewModel: WorkViewModelBase<LanguageSelectElement>
    {
        #region variable

        private LanguageItemViewModel _selectedLanguage;

        #endregion

        public LanguageSelectViewModel(LanguageSelectElement model)
            : base(model)
        {
            LanguageCollection = new ModelViewModelObservableCollectionManager<LanguageItemElement, LanguageItemViewModel>(Model.Languages, new ModelViewModelObservableCollectionOptions<LanguageItemElement, LanguageItemViewModel>() {
                ToViewModel = (m) => new LanguageItemViewModel(m),
            });
            LanguageItems = LanguageCollection.GetDefaultView();

            var language = Model.Languages.FirstOrDefault(a => CultureInfo.CurrentCulture == a.CultureInfo);
            if(language is not null && LanguageCollection.TryGetViewModel(language, out var selectedLanguage)) {
                this._selectedLanguage = selectedLanguage;
            } else {
                this._selectedLanguage = LanguageCollection.ViewModels.First();
            }
        }

        #region property

        private ModelViewModelObservableCollectionManager<LanguageItemElement, LanguageItemViewModel> LanguageCollection { get; }
        public ICollectionView LanguageItems { get; }

        public LanguageItemViewModel SelectedLanguage
        {
            get => this._selectedLanguage;
            set => SetProperty(ref this._selectedLanguage, value);
        }

        #endregion

        #region WorkViewModelBase

        #endregion
    }
}
