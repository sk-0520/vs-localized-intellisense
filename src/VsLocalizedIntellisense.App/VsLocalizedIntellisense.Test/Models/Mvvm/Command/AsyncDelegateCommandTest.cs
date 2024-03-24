using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Mvvm.Command;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Command
{

    [TestClass]
    public class AsyncDelegateCommandTest
    {
        #region function

        [TestMethod]
        public void Constructor_throw_Test()
        {
            var actual1 = Assert.ThrowsException<ArgumentNullException>(() => new AsyncDelegateCommand(null, null));
            Assert.AreEqual("executeAction", actual1.ParamName);

            var actual2 = Assert.ThrowsException<ArgumentNullException>(() => new AsyncDelegateCommand(null));
            Assert.AreEqual("executeAction", actual2.ParamName);

            var actual3 = Assert.ThrowsException<ArgumentNullException>(() => new AsyncDelegateCommand(o => Task.CompletedTask, null));
            Assert.AreEqual("canExecuteFunc", actual3.ParamName);
        }

        [TestMethod]
        public void ExecuteTest()
        {
            AsyncDelegateCommand command = null;
            command = new AsyncDelegateCommand(
                o => {
                    Assert.AreEqual(1, command.ExecutingCount);
                    Assert.IsFalse(command.CanExecute(null));
                    return Task.CompletedTask;
                }
            );
            command.Execute(null);
        }

        [TestMethod]
        public void SuppressCommandWhileExecutingTest()
        {
            AsyncDelegateCommand command = null;
            command = new AsyncDelegateCommand(
                o => {
                    Assert.AreEqual(1, command.ExecutingCount);
                    Assert.IsTrue(command.CanExecute(null));
                    return Task.CompletedTask;
                }
            ) {
                SuppressCommandWhileExecuting = false,
            };
            command.Execute(null);
        }

        [TestMethod]
        public void CanExecuteTest()
        {
            var command = new AsyncDelegateCommand(
                o => Task.CompletedTask,
                o => o != null
            );
            Assert.IsFalse(command.CanExecute(null));
            Assert.IsTrue(command.CanExecute(new object()));
        }
        #endregion
    }

    [TestClass]
    public class AsyncDelegateCommand_T_Test
    {
        #region function

        [TestMethod]
        public void ExecuteTest()
        {
            AsyncDelegateCommand<int> command = null;
            command = new AsyncDelegateCommand<int>(
                o => {
                    Assert.AreEqual(1, command.ExecutingCount);
                    Assert.AreEqual(100, o);
                    Assert.IsFalse(command.CanExecute(o));
                    return Task.CompletedTask;
                }
            );
            command.Execute(100);
        }

        [TestMethod]
        public void SuppressCommandWhileExecutingTest()
        {
            AsyncDelegateCommand<int> command = null;
            command = new AsyncDelegateCommand<int>(
                o => {
                    Assert.AreEqual(1, command.ExecutingCount);
                    Assert.AreEqual(100, o);
                    Assert.IsTrue(command.CanExecute(o));
                    return Task.CompletedTask;
                }
            ) {
                SuppressCommandWhileExecuting = false,
            };
            command.Execute(100);
        }

        [TestMethod]
        public void CanExecuteTest()
        {
            var command = new AsyncDelegateCommand<int>(
                o => Task.CompletedTask,
                o => o != 0
            );
            Assert.IsFalse(command.CanExecute(0));
            Assert.IsTrue(command.CanExecute(1));
        }

        #endregion
    }
}
