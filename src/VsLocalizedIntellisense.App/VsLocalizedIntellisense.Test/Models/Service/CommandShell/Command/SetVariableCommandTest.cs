using System;
using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    public class SetVariableCommandTest
    {
        #region function

        [Fact]
        public void NormalTest()
        {
            var test = new SetVariableCommand {
                VariableName = "var",
                Value = "abc",
            };
            var actual = test.GetStatement();
            Assert.Equal("set var=abc", actual);
            Assert.Equal("var", test.Variable.Name);
            Assert.Equal("%var%", test.Variable.Expression);
        }

        [Fact]
        public void IsExpressTest()
        {
            var test = new SetVariableCommand {
                VariableName = "var",
                Value = "123",
                IsExpress = true,
            };
            var actual = test.GetStatement();
            Assert.Equal("set /a var=123", actual);
            Assert.Equal("var", test.Variable.Name);
            Assert.Equal("%var%", test.Variable.Expression);
        }

        [Fact]
        public void VariableName_none_Test()
        {
            var test = new SetVariableCommand {
                VariableName = "",
            };
            Assert.Throws<InvalidOperationException>(() => test.GetStatement());
        }

        #endregion
    }
}
