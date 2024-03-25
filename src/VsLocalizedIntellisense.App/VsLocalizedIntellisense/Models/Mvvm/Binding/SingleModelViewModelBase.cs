using System.Runtime.CompilerServices;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Models.Mvvm.Binding
{
    /// <summary>
    /// モデルとビューモデルを一対一で紐づける。
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class SingleModelViewModelBase<TModel>: SimpleModelViewModelBase<TModel>
        where TModel : BindModelBase
    {
        protected SingleModelViewModelBase(TModel model, ILoggerFactory loggerFactory)
            : base(model)
        {
            LoggerFactory = loggerFactory;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        protected ILoggerFactory LoggerFactory { get; }
        protected ILogger Logger { get; }

        #endregion
    }
}
