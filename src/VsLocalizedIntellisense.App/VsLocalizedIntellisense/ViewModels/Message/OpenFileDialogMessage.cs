using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VsLocalizedIntellisense.Models.Mvvm.Message;

namespace VsLocalizedIntellisense.ViewModels.Message
{
    public enum OpenFileDialogKind
    {
        OpenFile,
        SaveFile,
        Directory,
    }

    public class OpenFileDialogMessage : IMessage
    {
        public OpenFileDialogMessage(string messageId = "")
        {
            MessageId = messageId;
        }

        #region property

        public OpenFileDialogKind Kind { get; set; }

        public DirectoryInfo CurrentDirectory { get; set; }
        public DirectoryInfo ResultDirectory { get; set; }

        #endregion

        #region IMessage

        public string MessageId { get; }

        #endregion

    }

    public static class OpenFileDialogMessageExtensions
    {
        #region function

        public static DirectoryInfo OpenDirectory(this OpenFileDialogMessage message, Window parentView)
        {
            Debug.Assert(message.Kind == OpenFileDialogKind.Directory);

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog()
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = message.CurrentDirectory != null
                    ? message.CurrentDirectory.FullName
                    : Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                ,
                ShowNewFolderButton = false,
            })
            {
                var result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    return new DirectoryInfo(dialog.SelectedPath);
                }
            }

            return null;
        }

        #endregion
    }
}
