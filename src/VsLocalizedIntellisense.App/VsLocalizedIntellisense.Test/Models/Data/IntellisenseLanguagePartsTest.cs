using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using VsLocalizedIntellisense.Models.Data;

namespace VsLocalizedIntellisense.Test.Models.Data
{
    public class IntellisenseLanguagePartsTest
    {
        #region function

        [Fact]
        public void Constructor_intellisenseVersion_Test()
        {
            var actual1 = Assert.Throws<ArgumentNullException>(() => new IntellisenseLanguageParts(null, "LIB", "LANG"));
            Assert.Equal("intellisenseVersion", actual1.ParamName);

            var actual2 = Assert.Throws<ArgumentException>(() => new IntellisenseLanguageParts("", "LIB", "LANG"));
            Assert.Equal("intellisenseVersion", actual2.ParamName);

            var actual3 = Assert.Throws<ArgumentException>(() => new IntellisenseLanguageParts(" ", "LIB", "LANG"));
            Assert.Equal("intellisenseVersion", actual3.ParamName);
        }

        [Fact]
        public void Constructor_libraryName_Test()
        {
            var actual1 = Assert.Throws<ArgumentNullException>(() => new IntellisenseLanguageParts("INTELLISENSE", null, "LANG"));
            Assert.Equal("libraryName", actual1.ParamName);

            var actual2 = Assert.Throws<ArgumentException>(() => new IntellisenseLanguageParts("INTELLISENSE", "", "LANG"));
            Assert.Equal("libraryName", actual2.ParamName);

            var actual3 = Assert.Throws<ArgumentException>(() => new IntellisenseLanguageParts("INTELLISENSE", " ", "LANG"));
            Assert.Equal("libraryName", actual3.ParamName);
        }

        [Fact]
        public void Constructor_language_Test()
        {
            var actual1 = Assert.Throws<ArgumentNullException>(() => new IntellisenseLanguageParts("INTELLISENSE", "LIB", null));
            Assert.Equal("language", actual1.ParamName);

            var actual2 = Assert.Throws<ArgumentException>(() => new IntellisenseLanguageParts("INTELLISENSE", "LIB", ""));
            Assert.Equal("language", actual2.ParamName);

            var actual3 = Assert.Throws<ArgumentException>(() => new IntellisenseLanguageParts("INTELLISENSE", "LIB", " "));
            Assert.Equal("language", actual3.ParamName);
        }

        [Theory]
        [InlineData("a/b/c", "a", "b", "c")]
        public void ToStringTest(string expected, string intellisenseVersion, string libraryName, string language)
        {
            var test = new IntellisenseLanguageParts(intellisenseVersion, libraryName, language);
            var actual = test.ToString();
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
