using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    [TestClass]
    public class ChangeSelfDirectoryCommandTest
    {
        #region function

        [TestMethod]
        public void Test()
        {
            var test = new ChangeSelfDirectoryCommand();
            var actual = test.GetStatement();
            Assert.AreEqual("cd /d %~dp0", actual);
        }

        #endregion
    }
}
