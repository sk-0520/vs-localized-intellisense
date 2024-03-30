using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    [TestClass]
    public class UrlHelperTest
    {
        #region function

        [TestMethod]
        [DataRow("http://localhost", "http://localhost", "")]
        [DataRow("http://localhost/", "http://localhost", "")]
        [DataRow("http://localhost/abc", "http://localhost", "abc")]
        [DataRow("http://localhost/abc", "http://localhost", "/abc")]
        [DataRow("http://localhost/abc", "http://localhost", "abc/")]
        [DataRow("http://localhost/abc", "http://localhost", "/abc/")]
        [DataRow("http://localhost/abc/def", "http://localhost", "abc", "def")]
        [DataRow("http://localhost/abc/def", "http://localhost", "abc", "/def")]
        [DataRow("http://localhost/abc/def", "http://localhost", "abc", "def/")]
        [DataRow("http://localhost/abc/def", "http://localhost", "abc", "/def/")]
        public void JoinUrlTest(string expectedUrl, string baseUrl, string path, params string[] paths)
        {
            var expected = new Uri(expectedUrl);
            var actual = UrlHelper.JoinUri(baseUrl, path, paths);
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }

}
