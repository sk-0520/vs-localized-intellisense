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
    public class PauseCommandTest
    {
        #region function

        [TestMethod]
        public void Test()
        {
            var command = new PauseCommand();
            var actual = command.GetStatement();
            Assert.AreEqual("pause", actual);
        }

        #endregion
    }
}
