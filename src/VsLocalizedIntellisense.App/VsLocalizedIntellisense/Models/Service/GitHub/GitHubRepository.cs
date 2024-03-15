using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models.Service.GitHub
{
    public class GitHubRepository
    {
        public GitHubRepository(string owner, string name)
        {
            Owner = owner;
            Name = name;
        }

        #region property

        public string Owner { get; }
        public string Name { get; }

        #endregion
    }
}
