using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace VsLocalizedIntellisense.Diff.Models.Element
{
    public abstract class WorkElementBase: ObservableObject
    {
        #region property

        public abstract WorkState CurrentState { get; }

        #endregion
    }
}
