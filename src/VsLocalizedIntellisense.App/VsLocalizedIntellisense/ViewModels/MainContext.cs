using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;
using VsLocalizedIntellisense.Models.Mvvm.Message;

namespace VsLocalizedIntellisense.ViewModels
{
    public enum ContextMode
    {
        Download,
        Install,
    }

    public abstract class ContextContentViewModelBase<TModel>: SingleModelViewModelBase<TModel>
        where TModel : BindModelBase
    {
        public ContextContentViewModelBase(TModel model, Action<ContextMode> changedCallback, ISendableMessenger messenger, AppConfiguration configuration, ILoggerFactory loggerFactory)
            : base(model, loggerFactory)
        {
            ChangedCallback = changedCallback;
            Messenger = messenger;
            Configuration = configuration;
        }

        #region property

        private Action<ContextMode> ChangedCallback { get; }
        protected ISendableMessenger Messenger { get; }
        protected AppConfiguration Configuration { get; }

        #endregion

        #region function

        protected void ChangeMode(ContextMode mode)
        {
            ChangedCallback(mode);
        }

        #endregion
    }
}
