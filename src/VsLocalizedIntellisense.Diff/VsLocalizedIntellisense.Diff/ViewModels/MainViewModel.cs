using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using VsLocalizedIntellisense.Diff.Models.Element;

namespace VsLocalizedIntellisense.Diff.ViewModels
{
    public class MainViewModel: ObservableObject
    {
        public MainViewModel(MainElement model)
        {
            Model  = model;
        }

        #region property

        private MainElement Model { get; }

        #endregion
    }
}
