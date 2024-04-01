using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    public class RemarkCommandTest
    {
        #region function

        [Theory]
        [InlineData("rem", "")]
        [InlineData("rem a", "a")]
        public void Test(string expected, string value)
        {
            var command = new RemarkCommand() {
                Comment = value,
            };
            var actual = command.GetStatement();
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
