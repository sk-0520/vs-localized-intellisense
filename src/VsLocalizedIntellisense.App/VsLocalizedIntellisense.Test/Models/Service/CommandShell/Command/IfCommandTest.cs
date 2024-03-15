using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    [TestClass]
    public class IfErrorLevelCommandTest
    {
        #region function

        [TestMethod]
        public void IfTest()
        {
            var test = new IfErrorLevelCommand
            {
                Level = 10
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.AreEqual(
                "if ERRORLEVEL 10 (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [TestMethod]
        public void IfNotTest()
        {
            var test = new IfErrorLevelCommand
            {
                Level = 10,
                IsNot = true,
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.AreEqual(
                "if not ERRORLEVEL 10 (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [TestMethod]
        public void IfElseTest()
        {
            var test = new IfErrorLevelCommand
            {
                Level = 10
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });
            test.FalseBlock.Add(new EchoCommand() { Value = "false" });

            var actual = test.GetStatement();
            Assert.AreEqual(
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

    [TestClass]
    public class IfExpressCommandTest
    {
        #region function

        [TestMethod]
        public void IfTest()
        {
            var test = new IfExpressCommand()
            {
                Left = "l",
                Right = "r",
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.AreEqual(
                "if l == r (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [TestMethod]
        public void IfNotTest()
        {
            var test = new IfExpressCommand()
            {
                Left = "l",
                Right = "r",
                IsNot = true,
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.AreEqual(
                "if not l == r (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [TestMethod]
        public void IfElseTest()
        {
            var test = new IfExpressCommand()
            {
                Left = "l",
                Right = "r"
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });
            test.FalseBlock.Add(new EchoCommand() { Value = "false" });

            var actual = test.GetStatement();
            Assert.AreEqual(
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

    [TestClass]
    public class IfExistCommandTest
    {
        #region function

        [TestMethod]
        public void IfTest()
        {
            var test = new IfExistCommand()
            {
                Path = "path",
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.AreEqual(
                "if exist path (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [TestMethod]
        public void IfNotTest()
        {
            var test = new IfExistCommand()
            {
                Path = "path",
                IsNot = true,
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });

            var actual = test.GetStatement();
            Assert.AreEqual(
                "if not exist path (" + Environment.NewLine +
                "\techo true" + Environment.NewLine +
                ")",
                actual
            );
        }

        [TestMethod]
        public void IfElseTest()
        {
            var test = new IfExistCommand()
            {
                Path = "path",
            };
            test.TrueBlock.Add(new EchoCommand() { Value = "true" });
            test.FalseBlock.Add(new EchoCommand() { Value = "false" });

            var actual = test.GetStatement();
            Assert.AreEqual(
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
