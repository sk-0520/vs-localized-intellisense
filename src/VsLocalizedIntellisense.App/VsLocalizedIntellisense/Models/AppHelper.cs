using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VsLocalizedIntellisense.Models
{
    public static class AppHelper
    {
        #region function

        /// <summary>
        /// アプリケーション再起動。
        /// </summary>
        public static void Reboot()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        #endregion
    }
}
