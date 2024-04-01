using System;
using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
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

        [Theory]
        [InlineData("test", false, false)]
        [InlineData("TEST", true, false)]
        [InlineData("@test", false, true)]
        [InlineData("@TEST", true, true)]
        public void GetStatementCommandNameTest(string expected, bool commandNameIsUpper, bool suppressCommand)
        {
            var test = new TestCommand() {
                CommandNameIsUpper = commandNameIsUpper,
                SuppressCommand = suppressCommand,
            };
            var actual = test.GetStatementCommandName();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CommandNameTest()
        {
            var test = new TestCommand();
            Assert.Equal("test", test.CommandName);
            Assert.Throws<NotSupportedException>(() => test.CommandName = "test");
        }

        #endregion
    }
}
