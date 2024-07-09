using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VsLocalizedIntellisense.Diff.ViewModels
{
    public abstract class ViewModelBase: ObservableObject
    {
        protected ViewModelBase()
            : base()
        { }
    }

    /// <summary>
    /// モデルとビューモデルを一対一で紐づける。
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class SimpleModelViewModelBase<TModel>: ViewModelBase
        where TModel : notnull
    {
        protected SimpleModelViewModelBase(TModel model)
            : base()
        {
            Model = model;
        }

        #region property

        protected TModel Model { get; private set; }

        #endregion

        #region function


        #endregion
    }
}
