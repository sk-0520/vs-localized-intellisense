using System;
using System.Runtime.Serialization;

namespace VsLocalizedIntellisense.Models.Data
{
    [DataContract]
    public class IntellisenseVersionData: ICachedTimestamp
    {
        #region property

        [DataMember]
        public string[] VersionItems { get; set; } = Array.Empty<string>();

        #endregion

        #region ICachedTimestamp

        [DataMember]
        public DateTimeOffset CachedTimestamp { get; set; } = DateTimeOffset.Now;

        #endregion
    }
}
