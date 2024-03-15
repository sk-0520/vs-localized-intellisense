using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VsLocalizedIntellisense.Models
{
    /// <summary>
    /// 通知モデル基底。
    /// </summary>
    public abstract class NotifyPropertyBase: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region property

        #endregion


        #region function

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged(string notifyPropertyName)
        {
            OnPropertyChanged(notifyPropertyName);
        }

        #endregion
    }
}
