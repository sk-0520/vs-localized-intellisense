using System;
using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    public class IfErrorLevelCommandTest
    {
        #region function

        [Fact]
        public void IfTest()
        {
            var test = new IfErrorLevelCommand {
                Level = 10
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.Equal(
                "if ERRORLEVEL 10 (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [Fact]
        public void IfNotTest()
        {
            var test = new IfErrorLevelCommand {
                Level = 10,
                IsNot = true,
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.Equal(
                "if not ERRORLEVEL 10 (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [Fact]
        public void IfElseTest()
        {
            var test = new IfErrorLevelCommand {
                Level = 10
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });
            test.FalseBlock.Add(new EchoCommand() { Value = "false" });

            var actual = test.GetStatement();
            Assert.Equal(
                "if ERRORLEVEL 10 (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ") else (" + Environment.NewLine +
                "\techo false" + Environment.NewLine +
                ")",
                actual
            );
        }

        #endregion
    }

    public class IfExpressCommandTest
    {
        #region function

        [Fact]
        public void IfTest()
        {
            var test = new IfExpressCommand() {
                Left = "l",
                Right = "r",
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.Equal(
                "if l == r (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [Fact]
        public void IfNotTest()
        {
            var test = new IfExpressCommand() {
                Left = "l",
                Right = "r",
                IsNot = true,
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.Equal(
                "if not l == r (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [Fact]
        public void IfElseTest()
        {
            var test = new IfExpressCommand() {
                Left = "l",
                Right = "r"
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });
            test.FalseBlock.Add(new EchoCommand() { Value = "false" });

            var actual = test.GetStatement();
            Assert.Equal(
                "if l == r (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ") else (" + Environment.NewLine +
                "\techo false" + Environment.NewLine +
                ")",
                actual
            );
        }

        #endregion
    }

    public class IfExistCommandTest
    {
        #region function

        [Fact]
        public void IfTest()
        {
            var test = new IfExistCommand() {
                Path = "path",
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.Equal(
                "if exist path (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [Fact]
        public void IfNotTest()
        {
            var test = new IfExistCommand() {
                Path = "path",
                IsNot = true,
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.Equal(
                "if not exist path (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [Fact]
        public void IfElseTest()
        {
            var test = new IfExistCommand() {
                Path = "path",
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });
            test.FalseBlock.Add(new EchoCommand() { Value = "false" });

            var actual = test.GetStatement();
            Assert.Equal(
                "if exist path (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ") else (" + Environment.NewLine +
                "\techo false" + Environment.NewLine +
                ")",
                actual
            );
        }

        #endregion
    }
}
