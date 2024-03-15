using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandPrompt.Command
{
    [TestClass]
    public class SwitchEchoCommandTest
    {
        #region function

        [TestMethod]
        [DataRow("@echo off", false)]
        [DataRow("@echo on", true)]
        public void Test(string expected, bool input)
        {
            var sw = new SwitchEchoCommand()
            {
                On = input,
            };
            var actual = sw.GetStatement();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
