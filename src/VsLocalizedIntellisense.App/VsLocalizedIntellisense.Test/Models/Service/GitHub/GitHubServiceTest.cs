using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Service.GitHub;

namespace VsLocalizedIntellisense.Test.Models.Service.GitHub
{
    [TestClass]
    public class GitHubServiceTest
    {
        #region function

        [TestMethod]
        [DataRow("", "", "")]
        [DataRow("a", "a", "")]
        [DataRow("a", "", "a")]
        [DataRow("a/b/c", "a", "b", "c")]
        [DataRow("a/b/c", "/a/", "/b/", "/c/")]
        [DataRow("a/b/c", "\\a\\", "\\b\\", "\\c\\")]
        public void JoinPathTest(string expected, string path1, string path2, params string[] pathN)
        {
            var ghs = new GitHubService(new GitHubRepository(string.Empty, string.Empty), new GitHubOptions(), NullLoggerFactory.Instance);
            var actual = ghs.JoinPath(path1, path2, pathN);
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
