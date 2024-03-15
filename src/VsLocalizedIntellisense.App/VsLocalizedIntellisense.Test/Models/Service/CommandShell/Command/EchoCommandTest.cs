using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandPrompt.Command
{
    [TestClass]
    public class EchoCommandTest
    {
        #region function

        [TestMethod]
        [DataRow("echo.", "")]
        [DataRow("echo a", "a")]
        public void Test(string expected, string value)
        {
            var command = new EchoCommand()
            {
                Value = value,
            };
            var actual = command.GetStatement();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
