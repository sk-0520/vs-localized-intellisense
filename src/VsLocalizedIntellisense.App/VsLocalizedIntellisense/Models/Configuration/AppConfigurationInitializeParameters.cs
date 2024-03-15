using System;
using System.Reflection;

namespace VsLocalizedIntellisense.Models.Configuration
{
    public class AppConfigurationInitializeParameters
    {
        #region variable

        AssemblyName _assemblyName;

        #endregion

        public AppConfigurationInitializeParameters(DateTime utcTimestamp)
        {
            UtcTimestamp = utcTimestamp;
            Assembly = Assembly.GetExecutingAssembly();
        }

        #region proeprty

        public DateTime UtcTimestamp { get; }

        public Assembly Assembly { get; set; }
        public AssemblyName AssemblyName
        {
            get
            {
                if(this._assemblyName == null) {
                    this._assemblyName = Assembly.GetName();
                }

                return this._assemblyName;
            }
        }

        #endregion
    }
}
