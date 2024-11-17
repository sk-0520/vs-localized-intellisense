using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Diff.Models.Binding
{
    /// <summary>
    /// ビューモデル適用前後。
    /// </summary>
    public enum ModelViewModelObservableCollectionViewModelApply
    {
        /// <summary>
        /// 実行前。
        /// </summary>
        Before,
        /// <summary>
        /// 実行後。
        /// </summary>
        After,
    }
}
