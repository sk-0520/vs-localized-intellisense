using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace VsLocalizedIntellisense.Diff.Models.Element
{
    public class LanguageSelectElement: WorkElementBase
    {
        public LanguageSelectElement(IConfiguration configuration)
        {
            Configuration = configuration;

            Languages = Configuration.GetRequiredSection("languages").Get<string[]>()!;
        }

        #region property

        private IConfiguration Configuration { get; }
        public IReadOnlyCollection<string> Languages { get; }

        #endregion

        #region function

        #endregion

        #region WorkElementBase

        public override WorkState CurrentState { get; } = WorkState.Language;

        #endregion
    }
}
