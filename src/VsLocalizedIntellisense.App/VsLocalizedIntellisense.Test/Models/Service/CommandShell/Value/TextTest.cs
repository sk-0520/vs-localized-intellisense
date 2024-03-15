using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Value
{
    [TestClass]
    public class TextTest
    {
        #region function

        [TestMethod]
        public void Test()
        {
            var test = new Text("abc");
            Assert.AreEqual("abc", test.Data);
            Assert.AreEqual("abc", test.Expression);
        }

        #endregion
    }
}
