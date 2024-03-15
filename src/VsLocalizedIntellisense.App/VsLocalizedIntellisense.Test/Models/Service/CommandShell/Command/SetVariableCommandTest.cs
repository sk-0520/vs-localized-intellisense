using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    [TestClass]
    public class SetVariableCommandTest
    {
        #region function

        [TestMethod]
        public void NormalTest()
        {
            var test = new SetVariableCommand {
                VariableName = "var",
                Value = "abc",
            };
            var actual = test.GetStatement();
            Assert.AreEqual("set var=abc", actual);
            Assert.AreEqual("var", test.Variable.Name);
            Assert.AreEqual("%var%", test.Variable.Expression);
        }

        [TestMethod]
        public void IsExpressTest()
        {
            var test = new SetVariableCommand {
                VariableName = "var",
                Value = "123",
                IsExpress = true,
            };
            var actual = test.GetStatement();
            Assert.AreEqual("set /a var=123", actual);
            Assert.AreEqual("var", test.Variable.Name);
            Assert.AreEqual("%var%", test.Variable.Expression);
        }

        [TestMethod]
        public void VariableName_none_Test()
        {
            var test = new SetVariableCommand {
                VariableName = "",
            };
            Assert.ThrowsException<InvalidOperationException>(() => test.GetStatement());
        }

        #endregion
    }
}
