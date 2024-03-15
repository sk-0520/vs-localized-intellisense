using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Value
{
    [TestClass]
    public class VariableTest
    {
        #region function

        [TestMethod]
        public void NormalTest()
        {
            var test = new Variable("abc");
            Assert.AreEqual("abc", test.Name);
            Assert.AreEqual("%abc%", test.Expression);

            test.Name = "xyz";
            Assert.AreEqual("xyz", test.Name);
            Assert.AreEqual("%xyz%", test.Expression);
        }

        [TestMethod]
        public void DelayedExpansionTest()
        {
            var test = new Variable("abc") {
                DelayedExpansion = true,
            };
            Assert.AreEqual("abc", test.Name);
            Assert.AreEqual("!abc!", test.Expression);

            test.Name = "xyz";
            Assert.AreEqual("xyz", test.Name);
            Assert.AreEqual("!xyz!", test.Expression);
        }

        [TestMethod]
        public void IsReadOnlyTest()
        {
            var test = new Variable("abc", true);
            Assert.AreEqual("abc", test.Name);
            Assert.AreEqual("%abc%", test.Expression);

            Assert.ThrowsException<InvalidOperationException>(() => test.Name = "xyz");
            Assert.AreEqual("abc", test.Name);
            Assert.AreEqual("%abc%", test.Expression);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.ThrowsException<ArgumentException>(() => new Variable(""));
        }

        [TestMethod]
        public void Name_throw_Test()
        {
            var test = new Variable("abc");

            Assert.ThrowsException<ArgumentException>(() => test.Name = "");
        }

        #endregion
    }
}
