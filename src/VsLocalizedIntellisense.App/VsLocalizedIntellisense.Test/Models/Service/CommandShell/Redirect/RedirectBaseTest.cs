using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Redirect;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Redirect
{
    [TestClass]
    public class RedirectBaseTest
    {
        private class TestRedirect: RedirectBase
        { }

        #region function

        [TestMethod]
        [DataRow("> a", "a", false)]
        [DataRow(">> a", "a", true)]
        public void ExpressionTest(string expected, string target, bool append)
        {
            var test = new TestRedirect() {
                Target = target,
                Append = append,
            };
            var actual = test.Expression;
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
