using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandPrompt.Command
{
    public class SwitchEchoCommandTest
    {
        #region function

        [Theory]
        [InlineData("@echo off", false)]
        [InlineData("@echo on", true)]
        public void Test(string expected, bool input)
        {
            var sw = new SwitchEchoCommand() {
                On = input,
            };
            var actual = sw.GetStatement();
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
