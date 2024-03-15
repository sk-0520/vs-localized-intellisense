using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Data
{
    public class IntellisenseLibraryData: ICachedTimestamp
    {
        #region property

        public string HashAlgorithm { get; set; }
        public byte[] HashValue { get; set; }

        #endregion

        #region ICachedTimestamp

        public DateTimeOffset CachedTimestamp { get; set; } = DateTimeOffset.Now;

        #endregion
    }
}
