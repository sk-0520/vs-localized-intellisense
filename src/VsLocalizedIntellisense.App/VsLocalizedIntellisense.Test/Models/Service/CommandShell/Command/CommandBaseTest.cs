using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    [TestClass]
    public class CommandBaseTest
    {
        private class TestCommand: CommandBase
        {
            public TestCommand()
                : base("test")
            { }

            public override string GetStatement()
            {
                return GetStatementCommandName();
            }
        }

        #region function

        [TestMethod]
        [DataRow("test", false, false)]
        [DataRow("TEST", true, false)]
        [DataRow("@test", false, true)]
        [DataRow("@TEST", true, true)]
        public void GetStatementCommandNameTest(string expected, bool commandNameIsUpper, bool suppressCommand)
        {
            var test = new TestCommand() {
                CommandNameIsUpper = commandNameIsUpper,
                SuppressCommand = suppressCommand,
            };
            var actual = test.GetStatementCommandName();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CommandNameTest()
        {
            var test = new TestCommand();
            Assert.AreEqual("test", test.CommandName);
            Assert.ThrowsException<NotSupportedException>(() => test.CommandName = "test");
        }

        #endregion
    }
}
