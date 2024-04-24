using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace VsLocalizedIntellisense.Diff.Models.Element
{
    public class LanguageSelectElement: WorkElementBase
    {
        public LanguageSelectElement(MainElement mainElement, IConfiguration configuration)
            : base(mainElement, configuration)
        {
            Languages = Configuration
                .GetRequiredSection("languages").Get<string[]>()!
                .Select(a => new CultureInfo(a))
                .Select(a => new LanguageItemElement(a))
                .ToObservableCollection();
            ;
        }

        #region property

        public ObservableCollection<LanguageItemElement> Languages { get; }

        #endregion

        #region function

        #endregion

        #region WorkElementBase

        public override WorkState CurrentState { get; } = WorkState.Language;

        #endregion
    }
}
