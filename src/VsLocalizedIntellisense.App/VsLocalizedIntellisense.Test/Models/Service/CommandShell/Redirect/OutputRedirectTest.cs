using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Redirect;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Redirect
{
    [TestClass]
    public class OutputRedirectTest
    {
        #region function

        [TestMethod]
        public void Expression_none_Test()
        {
            var test = new OutputRedirect();
            Assert.AreEqual("", test.Expression);
        }

        [TestMethod]
        public void Expression_std_Test()
        {
            var test = new OutputRedirect() {
                Target = "OUT",
            };
            Assert.AreEqual("> OUT", test.Expression);
        }

        [TestMethod]
        public void Expression_error_StandardOutput_Test()
        {
            var test = new OutputRedirect() {
                Error = new ErrorRedirect() {
                    StandardOutput = true,
                },
            };
            Assert.AreEqual("2>&1", test.Expression);

            test.Target = "OUT";
            Assert.AreEqual("> OUT 2>&1", test.Expression);
        }

        [TestMethod]
        public void Expression_error_redirect_Test()
        {
            var test = new OutputRedirect() {
                Error = new ErrorRedirect() {
                    Target = "ERR"
                },
            };
            Assert.AreEqual("2> ERR", test.Expression);

            test.Target = "OUT";
            Assert.AreEqual("> OUT 2> ERR", test.Expression);
        }

        [TestMethod]
        public void Expression_error_none_Test()
        {
            var test = new OutputRedirect() {
                Error = new ErrorRedirect(),
            };
            Assert.AreEqual("", test.Expression);
        }

        #endregion
    }
}
