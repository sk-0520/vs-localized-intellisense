using System;
using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Value
{
    public class VariableTest
    {
        #region function

        [Fact]
        public void NormalTest()
        {
            var test = new Variable("abc");
            Assert.Equal("abc", test.Name);
            Assert.Equal("%abc%", test.Expression);

            test.Name = "xyz";
            Assert.Equal("xyz", test.Name);
            Assert.Equal("%xyz%", test.Expression);
        }

        [Fact]
        public void DelayedExpansionTest()
        {
            var test = new Variable("abc") {
                DelayedExpansion = true,
            };
            Assert.Equal("abc", test.Name);
            Assert.Equal("!abc!", test.Expression);

            test.Name = "xyz";
            Assert.Equal("xyz", test.Name);
            Assert.Equal("!xyz!", test.Expression);
        }

        [Fact]
        public void IsReadOnlyTest()
        {
            var test = new Variable("abc", true);
            Assert.Equal("abc", test.Name);
            Assert.Equal("%abc%", test.Expression);

            Assert.Throws<InvalidOperationException>(() => test.Name = "xyz");
            Assert.Equal("abc", test.Name);
            Assert.Equal("%abc%", test.Expression);
        }

        [Fact]
        public void ConstructorTest()
        {
            Assert.Throws<ArgumentException>(() => new Variable(""));
        }

        [Fact]
        public void Name_throw_Test()
        {
            var test = new Variable("abc");

            Assert.Throws<ArgumentException>(() => test.Name = "");
        }

        #endregion
    }
}
