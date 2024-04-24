using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace VsLocalizedIntellisense.Diff.Models.Element
{
    public abstract class WorkElementBase: ObservableObject
    {
        public WorkElementBase(MainElement mainElement, IConfiguration configuration)
        {
            MainElement = mainElement;
            Configuration = configuration;
        }

        #region property

        protected MainElement MainElement { get; }

        protected IConfiguration Configuration { get; }


        public abstract WorkState CurrentState { get; }

        #endregion
    }
}
