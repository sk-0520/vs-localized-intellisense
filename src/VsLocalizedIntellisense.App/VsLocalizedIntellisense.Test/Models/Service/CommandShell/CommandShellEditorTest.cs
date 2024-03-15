using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell
{
    [TestClass]
    public class CommandShellEditorTest
    {
        #region function

        [TestMethod]
        public void CreateEmptyLineTest()
        {
            var test = new CommandShellEditor();
            test.CreateEmptyLine();

            Assert.AreEqual(0, test.Actions.Count);
        }

        [TestMethod]
        public void AddEmptyLineTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddEmptyLine();

            Assert.AreEqual(test.Actions[0], actual);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void AddEmptyLinesTest(int length)
        {
            var test = new CommandShellEditor();
            var actual = test.AddEmptyLines(length);

            Assert.AreEqual(test.Actions.Count, actual.Length);
            for (var i = 0; i < length; i++)
            {
                Assert.AreEqual(test.Actions[i], actual[i]);
            }
        }

        [TestMethod]
        public void AddEmptyLines_throw_Test()
        {
            var test = new CommandShellEditor();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => test.AddEmptyLines(-1));
        }


        #region add-command

        [TestMethod]
        public void AddChangeCodePageTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddChangeCodePage(Encoding.Default);

            Assert.AreEqual(test.Actions[0], actual);
            Assert.AreEqual(Encoding.Default, actual.Encoding);
        }

        [TestMethod]
        public void AddChangeDirectoryTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddChangeDirectory("dir");

            Assert.AreEqual(test.Actions[0], actual);
            Assert.AreEqual("dir", actual.Path.Expression);
            Assert.IsTrue(actual.WithDrive);
        }

        [TestMethod]
        public void AddChangeSelfDirectoryTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddChangeSelfDirectory();

            Assert.AreEqual(test.Actions[0], actual);
            Assert.AreEqual("cd /d %~dp0", actual.GetStatement());
        }

        [TestMethod]
        public void AddCopyTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddCopy("src", "dst");

            Assert.AreEqual(test.Actions[0], actual);
            Assert.AreEqual("src", actual.Source.Expression);
            Assert.AreEqual("dst", actual.Destination.Expression);
            Assert.AreEqual(PromptMode.Default, actual.PromptMode);
        }

        [TestMethod]
        public void AddEchoTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddEcho("abc");

            Assert.AreEqual(test.Actions[0], actual);
            Assert.AreEqual("abc", actual.Value.Expression);
        }

        [TestMethod]
        public void AddIfErrorLevelTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddIfErrorLevel(123);

            Assert.AreEqual(test.Actions[0], actual);
            Assert.AreEqual(123, actual.Level);
            Assert.IsFalse(actual.IsNot);
        }

        [TestMethod]
        public void AddIfExpressTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddIfExpress("LEFT", "RIGHT");

            Assert.AreEqual(test.Actions[0], actual);
            Assert.AreEqual("LEFT", actual.Left.Expression);
            Assert.AreEqual("RIGHT", actual.Right.Expression);
            Assert.IsFalse(actual.IsNot);
        }

        [TestMethod]
        public void AddIfExistTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddIfExist("path");

            Assert.AreEqual(test.Actions[0], actual);
            Assert.AreEqual("path", actual.Path.Expression);
            Assert.IsFalse(actual.IsNot);
        }

        [TestMethod]
        public void AddMakeDirectoryTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddMakeDirectory("abc");

            Assert.AreEqual(test.Actions[0], actual);
        }

        [TestMethod]
        public void AddPauseTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddPause();

            Assert.AreEqual(test.Actions[0], actual);
        }

        [TestMethod]
        public void AddRemarkTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddRemark("comment");

            Assert.AreEqual(test.Actions[0], actual);
            Assert.AreEqual("comment", actual.Comment.Expression);
        }


        [TestMethod]
        public void AddSetVariableTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddSetVariable("var", "value");

            Assert.AreEqual(test.Actions[0], actual);
            Assert.AreEqual("var", actual.VariableName);
            Assert.IsFalse(actual.IsExpress);
            Assert.IsFalse(actual.Variable.IsReadOnly);
            Assert.IsFalse(actual.Variable.DelayedExpansion);
        }

        [TestMethod]
        public void AddSwitchEchoTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddSwitchEcho(true);

            Assert.AreEqual(test.Actions[0], actual);
            Assert.IsTrue(actual.On);
        }

        #endregion

        [TestMethod]
        public void ToSourceCodeTest()
        {
            var test = new CommandShellEditor();
            test.AddSwitchEcho(false);
            test.AddEmptyLine();
            test.AddEcho("hello");
            test.AddEmptyLine();
            var pathIf = test.AddIfExist("path");
            pathIf.TrueBlock.Add(new EchoCommand() { Value = "TRUE" });
            pathIf.FalseBlock.Add(new EchoCommand() { Value = "FALSE" });
            test.AddCopy("src", "dst");
            test.AddEmptyLines(2);
            test.AddRemark("bye");

            var actual = test.ToSourceCode();
            var expected
                = "@echo off" + Environment.NewLine
                + Environment.NewLine
                + "echo hello" + Environment.NewLine
                + Environment.NewLine
                + "if exist path (" + Environment.NewLine
                + "\techo TRUE" + Environment.NewLine
                + ") else (" + Environment.NewLine
                + "\techo FALSE" + Environment.NewLine
                + ")" + Environment.NewLine
                + "copy src dst" + Environment.NewLine
                + Environment.NewLine
                + Environment.NewLine
                + "rem bye" + Environment.NewLine
                ;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task WriteAsyncTest()
        {
            var test = new CommandShellEditor();
            var unicode = new UnicodeEncoding(false, false);
            test.Options.Encoding = unicode;

            test.AddSwitchEcho(false);
            test.AddEmptyLine();
            test.AddEcho("hello");
            test.AddEmptyLine();
            var pathIf = test.AddIfExist("path");
            pathIf.TrueBlock.Add(new EchoCommand() { Value = "TRUE" });
            pathIf.FalseBlock.Add(new EchoCommand() { Value = "FALSE" });
            test.AddCopy("src", "dst");
            test.AddEmptyLines(2);
            test.AddRemark("bye");

            var expected = test.ToSourceCode();
            using (var dst = new MemoryStream())
            {
                await test.WriteAsync(dst);
                var actual = unicode.GetString(dst.ToArray());
            Assert.AreEqual(expected, actual);
            }
        }

        #endregion
    }
}
