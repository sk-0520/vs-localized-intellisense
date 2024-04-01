using System.Collections.Generic;
using Xunit;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    public class StringsTest
    {
        #region function

        [Theory]
        [InlineData("", "", "<", ">")]
        [InlineData("a", "a", "<", ">")]
        [InlineData("<a", "<a", "<", ">")]
        [InlineData("a>", "a>", "<", ">")]
        [InlineData("[a]", "<a>", "<", ">")]
        [InlineData("[a][b]", "<a><b>", "<", ">")]
        public void ReplacePlaceholderTest(string expected, string src, string head, string tail)
        {
            var actual = Strings.ReplacePlaceholder(src, head, tail, s => "[" + s + "]");
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("a", "a", "<", ">")]
        [InlineData("A", "<a>", "<", ">")]
        [InlineData("<aa>", "<aa>", "<", ">")]
        [InlineData("AB", "<a><b>", "<", ">")]
        [InlineData("<a<a>>B", "<a<a>><b>", "<", ">")]
        [InlineData("a", "a", "@[", "]")]
        [InlineData("A", "@[a]", "@[", "]")]
        [InlineData("@[aa]", "@[aa]", "@[", "]")]
        [InlineData("AB", "@[a]@[b]", "@[", "]")]
        [InlineData("@[a@[a]]B", "@[a@[a]]@[b]", "@[", "]")]
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
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("a", "${A}")]
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
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
