using System;
using System.Diagnostics;
using System.Windows;

namespace VsLocalizedIntellisense.Models.Mvvm.Message
{
    public class ViewMessenger<TView> : DisposerBase
        where TView : FrameworkElement
    {
        public ViewMessenger(TView element, Action<IReceivableMessenger> resisterAction)
        {
            Element = element;
            ResisterAction = resisterAction;

            Element.Unloaded += Element_Unloaded;
            if(Element is Window window)
            {
                window.Closed += Window_Closed;
            }

            Element.DataContextChanged += Element_DataContextChanged;

            if (Element.IsLoaded)
            {
                Register();
            }
        }

        #region property

        private TView Element { get; set; }
        private ScopedMessenger Messenger { get; set; }
        private Action<IReceivableMessenger> ResisterAction { get; set; }

        #endregion

        #region function

        private void Register()
        {
            ThrowIfDisposed();

            if(Element.DataContext == null)
            {
                return;
            }

            Messenger = (ScopedMessenger)MessengerHelper.GetMessengerFromProperty(Element.DataContext);
            ResisterAction(Messenger);
        }

        #endregion

        #region DisposerBase

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (Element != null)
                {
                    Element.DataContextChanged -= Element_DataContextChanged;

                    if (Element is Window window)
                    {
                        window.Closed -= Window_Closed;
                    }
                }
                Element = null;

                if (disposing)
                {
                    Messenger.Dispose();
                }
                Messenger = null;
                ResisterAction = null;
            }
            base.Dispose(disposing);
        }

        #endregion

        private void Element_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Messenger?.Dispose();

            if (e.NewValue != null)
            {
                Debug.Assert(e.NewValue == Element.DataContext);
                Register();
            }
        }

        private void Element_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Dispose();
        }

    }
}
