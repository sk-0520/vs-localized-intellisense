using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using VsLocalizedIntellisense.Diff.Models.Element;

namespace VsLocalizedIntellisense.Diff.ViewModels
{
    public abstract class WorkViewModelBase: ObservableObject
    { }

    public abstract class WorkViewModelBase<TModel>: WorkViewModelBase
        where TModel : WorkElementBase
    {
        protected WorkViewModelBase(TModel model)
        {
            Model = model;
        }

        #region property

        public TModel Model { get; }

        #endregion
    }
}
