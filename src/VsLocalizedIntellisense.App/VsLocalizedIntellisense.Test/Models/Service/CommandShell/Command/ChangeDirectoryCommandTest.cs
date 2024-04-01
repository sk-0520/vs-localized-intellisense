using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    public class ChangeDirectoryCommandTest
    {
        #region function

        [Fact]
        public void PathTest()
        {
            var test = new ChangeDirectoryCommand() {
                Path = "C:\\Windows",
            };
            var actual = test.GetStatement();
            Assert.Equal("cd C:\\Windows", actual);
        }

        [Fact]
        public void WithDriveTest()
        {
            var test = new ChangeDirectoryCommand() {
                Path = "Z:\\Windows",
                WithDrive = true,
            };
            var actual = test.GetStatement();
            Assert.Equal("cd /d Z:\\Windows", actual);
        }

        #endregion
    }
}
