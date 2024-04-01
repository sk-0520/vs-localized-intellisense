using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandPrompt.Command
{
    public class EchoCommandTest
    {
        #region function

        [Theory]
        [InlineData("echo.", "")]
        [InlineData("echo a", "a")]
        public void Test(string expected, string value)
        {
            var command = new EchoCommand() {
                Value = value,
            };
            var actual = command.GetStatement();
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
