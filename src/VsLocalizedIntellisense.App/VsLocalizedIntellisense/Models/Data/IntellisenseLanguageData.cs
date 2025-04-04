using System;
using System.Runtime.Serialization;

namespace VsLocalizedIntellisense.Models.Data
{
    [DataContract]
    public class IntellisenseLanguageData: ICachedTimestamp
    {
        #region property

        [DataMember]
        public string[] LanguageItems { get; set; } = Array.Empty<string>();

        #endregion

        #region ICachedTimestamp

        [DataMember]
        public DateTimeOffset CachedTimestamp { get; set; } = DateTimeOffset.Now;

        #endregion
    }
}
