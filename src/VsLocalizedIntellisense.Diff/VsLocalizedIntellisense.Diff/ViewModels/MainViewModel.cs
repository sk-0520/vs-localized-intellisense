using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;
using VsLocalizedIntellisense.Diff.Models;
using VsLocalizedIntellisense.Diff.Models.Element;

namespace VsLocalizedIntellisense.Diff.ViewModels
{
    public class MainViewModel: ObservableObject
    {
        #region variable

        private WorkViewModelBase? _workViewModel;

        #endregion

        public MainViewModel(MainElement model, IConfiguration configuration)
        {
            Model = model;
            Configuration = configuration;

        }

        #region property

        private MainElement Model { get; }
        private IConfiguration Configuration { get; }

        public WorkViewModelBase WorkViewModel
        {
            get
            {
                if(this._workViewModel is not null) {
                    if(this._workViewModel.SelfState == Model.WorkState) {
                        return this._workViewModel;
                    }
                }

                this._workViewModel = Model.WorkState switch {
                    WorkState.Language => new LanguageSelectViewModel(
                        new LanguageSelectElement(Model, Configuration)
                    ),
                    _ => throw new NotImplementedException(),
                };

                return this._workViewModel;
            }
        }

        #endregion
    }
}
