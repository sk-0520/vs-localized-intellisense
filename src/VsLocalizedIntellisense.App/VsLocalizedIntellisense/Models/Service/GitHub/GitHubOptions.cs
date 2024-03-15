using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Service.GitHub
{
    public class GitHubOptions
    {
        #region property

        public GitHubAuthentication Authentication { get; set; }
        public IDictionary<string, string> RequestHeaders { get; set; } = new Dictionary<string, string>();

        #endregion
    }
}
