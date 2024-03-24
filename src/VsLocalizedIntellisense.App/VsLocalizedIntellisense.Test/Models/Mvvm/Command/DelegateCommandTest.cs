using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Mvvm.Command;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Command
{
    [TestClass]
    public class DelegateCommandTest
    {
        #region function

        [TestMethod]
        public void ExecuteTest()
        {
            DelegateCommand command = null;
            command = new DelegateCommand(
                o => {
                    Assert.AreEqual(1, command.ExecutingCount);
                    Assert.IsFalse(command.CanExecute(null));
                }
            );
            command.Execute(null);
        }

        [TestMethod]
        public void SuppressCommandWhileExecutingTest()
        {
            DelegateCommand command = null;
            command = new DelegateCommand(
                o => {
                    Assert.AreEqual(1, command.ExecutingCount);
                    Assert.IsTrue(command.CanExecute(null));
                }
            ) {
                SuppressCommandWhileExecuting = false,
            };
            command.Execute(null);
        }

        [TestMethod]
        public void CanExecuteTest()
        {
            var command = new DelegateCommand(
                o => { },
                o => o != null
            );
            Assert.IsFalse(command.CanExecute(null));
            Assert.IsTrue(command.CanExecute(new object()));
        }

        #endregion
    }

    [TestClass]
    public class DelegateCommand_T_Test
    {
        #region function

        [TestMethod]
        public void ExecuteTest()
        {
            DelegateCommand<int> command = null;
            command = new DelegateCommand<int>(
                o => {
                    Assert.AreEqual(1, command.ExecutingCount);
                    Assert.AreEqual(100, o);
                    Assert.IsFalse(command.CanExecute(o));
                }
            );
            command.Execute(100);
        }

        [TestMethod]
        public void SuppressCommandWhileExecutingTest()
        {
            DelegateCommand<int> command = null;
            command = new DelegateCommand<int>(
                o => {
                    Assert.AreEqual(1, command.ExecutingCount);
                    Assert.AreEqual(100, o);
                    Assert.IsTrue(command.CanExecute(o));
                }
            ) {
                SuppressCommandWhileExecuting = false,
            };
            command.Execute(100);
        }

        [TestMethod]
        public void CanExecuteTest()
        {
            var command = new DelegateCommand<int>(
                o => { },
                o => o != 0
            );
            Assert.IsFalse(command.CanExecute(0));
            Assert.IsTrue(command.CanExecute(1));
        }

        #endregion
    }

}
