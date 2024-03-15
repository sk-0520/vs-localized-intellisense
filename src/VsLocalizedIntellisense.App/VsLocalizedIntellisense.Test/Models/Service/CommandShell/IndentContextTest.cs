using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandPrompt
{
    [TestClass]
    public class IndentContextTest
    {
        #region function

        [TestMethod]
        public void NestTest()
        {
            var root = new IndentContext();
            var child = root.Nest();

            Assert.AreEqual(root.Space, child.Space);
            Assert.AreEqual(0, root.Level);
            Assert.AreEqual(1, child.Level);
        }

        #endregion
    }
}
