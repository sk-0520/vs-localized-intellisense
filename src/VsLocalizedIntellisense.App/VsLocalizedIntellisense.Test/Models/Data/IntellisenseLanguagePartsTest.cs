using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Data;

namespace VsLocalizedIntellisense.Test.Models.Data
{
    [TestClass]
    public class IntellisenseLanguagePartsTest
    {
        #region function

        [TestMethod]
        public void Constructor_intellisenseVersion_Test()
        {
            var actual1 = Assert.ThrowsException<ArgumentNullException>(() => new IntellisenseLanguageParts(null, "LIB", "LANG"));
            Assert.AreEqual("intellisenseVersion", actual1.ParamName);

            var actual2 = Assert.ThrowsException<ArgumentException>(() => new IntellisenseLanguageParts("", "LIB", "LANG"));
            Assert.AreEqual("intellisenseVersion", actual2.ParamName);

            var actual3 = Assert.ThrowsException<ArgumentException>(() => new IntellisenseLanguageParts(" ", "LIB", "LANG"));
            Assert.AreEqual("intellisenseVersion", actual3.ParamName);
        }

        [TestMethod]
        public void Constructor_libraryName_Test()
        {
            var actual1 = Assert.ThrowsException<ArgumentNullException>(() => new IntellisenseLanguageParts("INTELLISENSE", null, "LANG"));
            Assert.AreEqual("libraryName", actual1.ParamName);

            var actual2 = Assert.ThrowsException<ArgumentException>(() => new IntellisenseLanguageParts("INTELLISENSE", "", "LANG"));
            Assert.AreEqual("libraryName", actual2.ParamName);

            var actual3 = Assert.ThrowsException<ArgumentException>(() => new IntellisenseLanguageParts("INTELLISENSE", " ", "LANG"));
            Assert.AreEqual("libraryName", actual3.ParamName);
        }

        [TestMethod]
        public void Constructor_language_Test()
        {
            var actual1 = Assert.ThrowsException<ArgumentNullException>(() => new IntellisenseLanguageParts("INTELLISENSE", "LIB", null));
            Assert.AreEqual("language", actual1.ParamName);

            var actual2 = Assert.ThrowsException<ArgumentException>(() => new IntellisenseLanguageParts("INTELLISENSE", "LIB", ""));
            Assert.AreEqual("language", actual2.ParamName);

            var actual3 = Assert.ThrowsException<ArgumentException>(() => new IntellisenseLanguageParts("INTELLISENSE", "LIB", " "));
            Assert.AreEqual("language", actual3.ParamName);
        }

        [TestMethod]
        [DataRow("a/b/c", "a", "b", "c")]
        public void ToStringTest(string expected, string intellisenseVersion, string libraryName, string language)
        {
            var test = new IntellisenseLanguageParts(intellisenseVersion, libraryName, language);
            var actual = test.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
