using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Mvvm.Binding
{
    /// <summary>
    /// 監視プロパティ設定。
    /// </summary>
    public class ObserveProperties
    {
        /// <summary>
        /// 生成。
        /// </summary>
        /// <param name="property">対象プロパティ。</param>
        /// <param name="attributes">監視対象プロパティ。</param>
        public ObserveProperties(PropertyInfo property, IReadOnlyCollection<ObservePropertyAttribute> attributes)
        {
            Property = property;
            Attributes = attributes;
        }

        #region proeprty

        /// <summary>
        /// 対象プロパティ。
        /// </summary>
        public PropertyInfo Property { get; }
        /// <summary>
        /// 監視対象プロパティ。
        /// </summary>
        public IReadOnlyCollection<ObservePropertyAttribute> Attributes { get; }

        #endregion
    }
}
