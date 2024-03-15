using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    [TestClass]
    public class ChangeDirectoryCommandTest
    {
        #region function

        [TestMethod]
        public void PathTest()
        {
            var test = new ChangeDirectoryCommand() {
                Path = "C:\\Windows",
            };
            var actual = test.GetStatement();
            Assert.AreEqual("cd C:\\Windows", actual);
        }

        [TestMethod]
        public void WithDriveTest()
        {
            var test = new ChangeDirectoryCommand() {
                Path = "Z:\\Windows",
                WithDrive = true,
            };
            var actual = test.GetStatement();
            Assert.AreEqual("cd /d Z:\\Windows", actual);
        }

        #endregion
    }
}
