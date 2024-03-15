using System;
using System.Threading;
using System.Windows.Input;

namespace VsLocalizedIntellisense.Models.Binding
{
    /// <summary>
    /// <see cref="ICommand"/>実装基底クラス。
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        protected CommandBase()
        {
            SynchronizationContext = SynchronizationContext.Current;
        }

        #region property

        protected SynchronizationContext SynchronizationContext { get; }

        /// <summary>
        /// 同時実行を抑制するか。
        /// <para>基本的に <see langword="init"/> であることを前提としてる。使えんけど。</para>
        /// </summary>
        public bool SuppressCommandWhileExecuting { get; set; } = true;

        #endregion

        #region function

        protected virtual void OnCanExecuteChanged()
        {
            if (SynchronizationContext != SynchronizationContext.Current)
            {
                SynchronizationContext.Post(o => CanExecuteChanged.Invoke(this, EventArgs.Empty), null);
            }
            else
            {
                CanExecuteChanged.Invoke(this, EventArgs.Empty);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        #endregion

        #region ICommand

#pragma warning disable CS0067 // イベント 'CommandBase.CanExecuteChanged' は使用されていません
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067

        public abstract void Execute(object parameter);

        public abstract bool CanExecute(object parameter);

        #endregion
    }
}
