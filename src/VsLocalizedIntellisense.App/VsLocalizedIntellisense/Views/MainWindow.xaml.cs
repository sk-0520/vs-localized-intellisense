using System.Windows;
using VsLocalizedIntellisense.Models.Mvvm.Message;
using VsLocalizedIntellisense.ViewModels.Message;

namespace VsLocalizedIntellisense.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow: Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Messenger = new ViewMessenger<Window>(this, m => {
                m.Register<OpenFileDialogMessage>(a => {
                    var dir = a.OpenDirectory(this);
                    a.ResultDirectory = dir;
                });
                m.Register<ScrollMessage>(a => {
                    if(this.listLogs.Items.Count == 0) {
                        return;
                    }
                    var item = this.listLogs.Items[this.listLogs.Items.Count - 1];
                    this.listLogs.ScrollIntoView(item);
                });
            });
        }

        #region property

        private ViewMessenger<Window> Messenger { get; set; }

        #endregion

        //private void listLogs_AddingNewItem(object sender, AddingNewItemEventArgs e)
        //{
        //    var item = this.listLogs.Items[this.listLogs.Items.Count - 1];
        //    this.listLogs.ScrollIntoView(item);
        //}
    }
}
