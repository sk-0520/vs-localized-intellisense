using System.ComponentModel;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;
using VsLocalizedIntellisense.Models.Mvvm.Binding.Collection;

namespace VsLocalizedIntellisense.ViewModels
{
    public class DirectoryViewModel: SingleModelViewModelBase<DirectoryElement>
    {
        public DirectoryViewModel(DirectoryElement model, ILoggerFactory loggerFactory)
            : base(model, loggerFactory)
        {
            LibraryVersionCollection = new ModelViewModelObservableCollectionManager<LibraryVersionElement, LibraryVersionViewModel>(Model.LibraryVersionItems, new ModelViewModelObservableCollectionOptions<LibraryVersionElement, LibraryVersionViewModel>() {
                ToViewModel = m => new LibraryVersionViewModel(m, LoggerFactory),
            });

            IntellisenseVersionCollection = new ModelViewModelObservableCollectionManager<IntellisenseVersionElement, IntellisenseVersionViewModel>(Model.IntellisenseVersionItems, new ModelViewModelObservableCollectionOptions<IntellisenseVersionElement, IntellisenseVersionViewModel>() {
                ToViewModel = m => new IntellisenseVersionViewModel(m, LoggerFactory),
            });

            LanguageCollection = new ModelViewModelObservableCollectionManager<LanguageElement, LanguageViewModel>(Model.LanguageItems, new ModelViewModelObservableCollectionOptions<LanguageElement, LanguageViewModel>() {
                ToViewModel = m => new LanguageViewModel(m, LoggerFactory),
            });

            Model.PropertyChanged += Model_PropertyChanged;
        }

        #region property

        public string DirectoryName => Model.Directory.Name;

        public bool IsDownloadTarget
        {
            get => Model.IsDownloadTarget;
            set => SetModel(value);
        }

        public double DownloadPercent
        {
            get => Model.DownloadPercent;
        }

        private ModelViewModelObservableCollectionManager<LibraryVersionElement, LibraryVersionViewModel> LibraryVersionCollection { get; }
        public ICollectionView LibraryVersionItems => LibraryVersionCollection.GetDefaultView();

        public LibraryVersionViewModel LibraryVersion
        {
            get
            {
                LibraryVersionCollection.TryGetViewModel(Model.LibraryVersion, out var result);
                return result;
            }
            set
            {
                if(LibraryVersionCollection.TryGetModel(value, out var model)) {
                    SetModel(model);
                }
            }
        }

        private ModelViewModelObservableCollectionManager<IntellisenseVersionElement, IntellisenseVersionViewModel> IntellisenseVersionCollection { get; }
        public ICollectionView IntellisenseVersionItems => IntellisenseVersionCollection.GetDefaultView();
        public IntellisenseVersionViewModel IntellisenseVersion
        {
            get
            {
                IntellisenseVersionCollection.TryGetViewModel(Model.IntellisenseVersion, out var result);
                return result;
            }
            set
            {
                if(IntellisenseVersionCollection.TryGetModel(value, out var model)) {
                    SetModel(model);
                }
            }
        }

        private ModelViewModelObservableCollectionManager<LanguageElement, LanguageViewModel> LanguageCollection { get; }
        public ICollectionView LanguageItems => LanguageCollection.GetDefaultView();
        public LanguageViewModel Language
        {
            get
            {
                LanguageCollection.TryGetViewModel(Model.Language, out var result);
                return result;
            }
            set
            {
                if(LanguageCollection.TryGetModel(value, out var model)) {
                    SetModel(model);
                }
            }
        }

        #endregion

        #region function

        public void ResetPercent()
        {
            Model.DownloadPercent = 0;
            RaisePropertyChanged(nameof(Model.DownloadPercent));
        }

        #endregion

        #region SingleModelViewModelBase

        protected override void Dispose(bool disposing)
        {
            if(!IsDisposed) {
                if(Model != null) {
                    Model.PropertyChanged -= Model_PropertyChanged;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Model.DownloadPercent)) {
                RaisePropertyChanged(nameof(DownloadPercent));
            }
        }


    }
}
