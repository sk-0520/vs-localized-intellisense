using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Models.Mvvm.Binding
{
    /// <summary>
    /// モデルとビューモデルを一対一で紐づける。
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class SimpleModelViewModelBase<TModel>: ViewModelBase
    {
        protected SimpleModelViewModelBase(TModel model, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            Model = model;
        }

        #region property

        protected TModel Model { get; private set; }

        #endregion

        #region function

        /// <summary>
        /// モデルの値を変更する。
        /// </summary>
        /// <typeparam name="TValue">設定値の型。</typeparam>
        /// <param name="value">設定値。</param>
        /// <param name="modelPropertyName">モデルのプロパティ名。</param>
        /// <param name="notifyPropertyName">変更通知のプロパティ名。基本的に呼び出し側のメンバ名となる想定。</param>
        /// <returns>変更されたか。</returns>
        protected bool SetModel<T>(T value, [CallerMemberName] string modelPropertyName = "", [CallerMemberName] string notifyPropertyName = "")
        {
            var type = Model.GetType();
            var prop = type.GetProperty(modelPropertyName);

            return ChangePropertyValue(Model, value, prop, notifyPropertyName);
        }

        #endregion

        #region ViewModelBase

        protected override void Dispose(bool disposing)
        {
            if(!IsDisposed) {
                Model = default;
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
