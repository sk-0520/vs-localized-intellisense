using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Values
{
    [TestClass]
    public class ExpressTest
    {
        #region function

        [TestMethod]
        public void ImplicitOperatorTest()
        {
            Express test = "abc";
            Assert.AreEqual(1, test.Values.Count);
            Assert.IsInstanceOfType<Text>(test.Values[0]);
            Assert.AreEqual("abc", test.Expression);
        }

        [TestMethod]
        public void OperatorTest()
        {
            var text = new Text("abc");
            var variable = new Variable("xyz");
            var test1 = text + variable;
            var test2 = variable + text;
            var test3 = text + variable + text;
            Assert.AreEqual("abc%xyz%", test1.Expression);
            Assert.AreEqual("%xyz%abc", test2.Expression);
            Assert.AreEqual("abc%xyz%abc", test3.Expression);

            var test4 = test1 + test2 + test3;
            Assert.AreEqual("abc%xyz%%xyz%abcabc%xyz%abc", test4.Expression);
        }

        #endregion
    }
}
