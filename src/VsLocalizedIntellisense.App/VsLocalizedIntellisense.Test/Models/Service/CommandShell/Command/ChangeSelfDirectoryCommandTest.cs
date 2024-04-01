using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    public class ChangeSelfDirectoryCommandTest
    {
        #region function

        [Fact]
        public void Test()
        {
            var test = new ChangeSelfDirectoryCommand();
            var actual = test.GetStatement();
            Assert.Equal("cd /d %~dp0", actual);
        }

        #endregion
    }
}
