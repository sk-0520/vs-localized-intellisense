using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    [TestClass]
    public class StringsTest
    {
        #region function

        [TestMethod]
        [DataRow("", "", "<", ">")]
        [DataRow("a", "a", "<", ">")]
        [DataRow("<a", "<a", "<", ">")]
        [DataRow("a>", "a>", "<", ">")]
        [DataRow("[a]", "<a>", "<", ">")]
        [DataRow("[a][b]", "<a><b>", "<", ">")]
        public void ReplacePlaceholderTest(string expected, string src, string head, string tail)
        {
            var actual = Strings.ReplacePlaceholder(src, head, tail, s => "[" + s + "]");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("a", "a", "<", ">")]
        [DataRow("A", "<a>", "<", ">")]
        [DataRow("<aa>", "<aa>", "<", ">")]
        [DataRow("AB", "<a><b>", "<", ">")]
        [DataRow("<a<a>>B", "<a<a>><b>", "<", ">")]
        [DataRow("a", "a", "@[", "]")]
        [DataRow("A", "@[a]", "@[", "]")]
        [DataRow("@[aa]", "@[aa]", "@[", "]")]
        [DataRow("AB", "@[a]@[b]", "@[", "]")]
        [DataRow("@[a@[a]]B", "@[a@[a]]@[b]", "@[", "]")]
        public void ReplaceRangeFromDictionaryTest(string expected, string src, string head, string tail)
        {
            var map = new Dictionary<string, string>() {
                ["A"] = "a",
                ["B"] = "b",
                ["C"] = "c",
                ["D"] = "d",
                ["E"] = "e",
                ["a"] = "A",
                ["b"] = "B",
                ["c"] = "C",
                ["d"] = "D",
                ["e"] = "E",
            };
            var actual = Strings.ReplacePlaceholderFromDictionary(src, head, tail, map);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("", "")]
        [DataRow("a", "${A}")]
        public void ReplaceFromDictionaryTest(string expected, string src)
        {
            var map = new Dictionary<string, string>() {
                ["A"] = "a",
                ["B"] = "b",
                ["C"] = "c",
                ["D"] = "d",
                ["E"] = "e",
                ["a"] = "A",
                ["b"] = "B",
                ["c"] = "C",
                ["d"] = "D",
                ["e"] = "E",
            };
            var actual = Strings.ReplaceFromDictionary(src, map);
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
