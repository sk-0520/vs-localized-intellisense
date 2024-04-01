using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    public class UrlHelperTest
    {
        #region function

        [Theory]
        [InlineData("http://localhost", "http://localhost", "")]
        [InlineData("http://localhost/", "http://localhost", "")]
        [InlineData("http://localhost/abc", "http://localhost", "abc")]
        [InlineData("http://localhost/abc", "http://localhost", "/abc")]
        [InlineData("http://localhost/abc", "http://localhost", "abc/")]
        [InlineData("http://localhost/abc", "http://localhost", "/abc/")]
        [InlineData("http://localhost/abc/def", "http://localhost", "abc", "def")]
        [InlineData("http://localhost/abc/def", "http://localhost", "abc", "/def")]
        [InlineData("http://localhost/abc/def", "http://localhost", "abc", "def/")]
        [InlineData("http://localhost/abc/def", "http://localhost", "abc", "/def/")]
        public void JoinUrlTest(string expectedUrl, string baseUrl, string path, params string[] paths)
        {
            var expected = new Uri(expectedUrl);
            var actual = UrlHelper.JoinUri(baseUrl, path, paths);
            Assert.Equal(expected, actual);
        }

        #endregion
    }

}
