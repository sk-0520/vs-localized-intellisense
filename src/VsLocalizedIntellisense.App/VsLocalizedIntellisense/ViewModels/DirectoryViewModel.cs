using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;
using VsLocalizedIntellisense.Models.Mvvm.Binding.Collection;

namespace VsLocalizedIntellisense.ViewModels
{
    public class DirectoryViewModel : SingleModelViewModelBase<DirectoryElement>
    {
        public DirectoryViewModel(DirectoryElement model, ILoggerFactory loggerFactory)
            : base(model, loggerFactory)
        {
            LibraryVersionCollection = new ModelViewModelObservableCollectionManager<LibraryVersionElement, LibraryVersionViewModel>(Model.LibraryVersionItems, new ModelViewModelObservableCollectionOptions<LibraryVersionElement, LibraryVersionViewModel>()
            {
                ToViewModel = m => new LibraryVersionViewModel(m, LoggerFactory),
            });

            IntellisenseVersionCollection = new ModelViewModelObservableCollectionManager<IntellisenseVersionElement, IntellisenseVersionViewModel>(Model.IntellisenseVersionItems, new ModelViewModelObservableCollectionOptions<IntellisenseVersionElement, IntellisenseVersionViewModel>()
            {
                ToViewModel = m => new IntellisenseVersionViewModel(m, LoggerFactory),
            });

            LanguageCollection = new ModelViewModelObservableCollectionManager<LanguageElement, LanguageViewModel>(Model.LanguageItems, new ModelViewModelObservableCollectionOptions<LanguageElement, LanguageViewModel>()
            {
                ToViewModel = m => new LanguageViewModel(m, LoggerFactory),
            });
        }

        #region property

        public string DirectoryName => Model.Directory.Name;

        public bool IsDownloadTarget
        {
            get => Model.IsDownloadTarget;
            set => SetModel(value);
        }

        private ModelViewModelObservableCollectionManager<LibraryVersionElement, LibraryVersionViewModel> LibraryVersionCollection { get; }
        public ICollectionView LibraryVersionItems => LibraryVersionCollection.GetDefaultView();

        public LibraryVersionViewModel LibraryVersion
        {
            get
            {
                return LibraryVersionCollection.GetViewModel(Model.LibraryVersion);
            }
            set
            {
                var model = LibraryVersionCollection.GetModel(value);
                SetModel(model);
            }
        }

        private ModelViewModelObservableCollectionManager<IntellisenseVersionElement, IntellisenseVersionViewModel> IntellisenseVersionCollection { get; }
        public ICollectionView IntellisenseVersionItems => IntellisenseVersionCollection.GetDefaultView();
        public IntellisenseVersionViewModel IntellisenseVersion
        {
            get
            {
                return IntellisenseVersionCollection.GetViewModel(Model.IntellisenseVersion);
            }
            set
            {
                var model = IntellisenseVersionCollection.GetModel(value);
                SetModel(model);
            }
        }

        private ModelViewModelObservableCollectionManager<LanguageElement, LanguageViewModel> LanguageCollection { get; }
        public ICollectionView LanguageItems => LanguageCollection.GetDefaultView();
        public LanguageViewModel Language
        {
            get
            {
                return LanguageCollection.GetViewModel(Model.Language);
            }
            set
            {
                var model = LanguageCollection.GetModel(value);
                SetModel(model);
            }
        }

        #endregion
    }
}
