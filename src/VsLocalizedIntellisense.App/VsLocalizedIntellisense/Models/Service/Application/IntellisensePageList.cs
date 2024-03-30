using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Service.Application
{
    [DataContract]
    public class IntellisensePageList
    {
        #region property

        [DataMember(Name = "directory")]
        public string[] Directories { get; set; } = Array.Empty<string>();

        [DataMember(Name = "file")]
        public string[] Files { get; set; } = Array.Empty<string>();

        #endregion
    }
}
