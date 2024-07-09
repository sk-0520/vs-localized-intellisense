using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace VsLocalizedIntellisense.Diff.Models.Element
{
    public class MainElement: ObservableObject
    {
        #region variable

        private WorkState _workState = WorkState.Language;

        #endregion

        public MainElement(IAppPath appPath, IConfiguration configuration)
        {
            AppPath = appPath;
            Configuration = configuration;
        }

        #region property

        public WorkState WorkState
        {
            get => this._workState;
            private set => SetProperty(ref this._workState, value);
        }

        private IAppPath AppPath { get; }
        private IConfiguration Configuration { get; }

        #endregion

        #region function

        #endregion
    }
}