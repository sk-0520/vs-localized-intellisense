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
    public class RemarkCommandTest
    {
        #region function

        [TestMethod]
        [DataRow("rem", "")]
        [DataRow("rem a", "a")]
        public void Test(string expected, string value)
        {
            var command = new RemarkCommand()
            {
                Comment = value,
            };
            var actual = command.GetStatement();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
