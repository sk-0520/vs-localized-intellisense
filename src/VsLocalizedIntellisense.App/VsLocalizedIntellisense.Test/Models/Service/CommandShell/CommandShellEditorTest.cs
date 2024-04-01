using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell
{
    public class CommandShellEditorTest
    {
        #region function

        [Fact]
        public void CreateEmptyLineTest()
        {
            var test = new CommandShellEditor();
            test.CreateEmptyLine();

            Assert.Equal(0, test.Actions.Count);
        }

        [Fact]
        public void AddEmptyLineTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddEmptyLine();

            Assert.Equal(test.Actions[0], actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void AddEmptyLinesTest(int length)
        {
            var test = new CommandShellEditor();
            var actual = test.AddEmptyLines(length);

            Assert.Equal(test.Actions.Count, actual.Length);
            for(var i = 0; i < length; i++) {
                Assert.Equal(test.Actions[i], actual[i]);
            }
        }

        [Fact]
        public void AddEmptyLines_throw_Test()
        {
            var test = new CommandShellEditor();
            Assert.Throws<ArgumentOutOfRangeException>(() => test.AddEmptyLines(-1));
        }


        #region add-command

        [Fact]
        public void AddChangeCodePageTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddChangeCodePage(Encoding.Default);

            Assert.Equal(test.Actions[0], actual);
            Assert.Equal(Encoding.Default, actual.Encoding);
        }

        [Fact]
        public void AddChangeDirectoryTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddChangeDirectory("dir");

            Assert.Equal(test.Actions[0], actual);
            Assert.Equal("dir", actual.Path.Expression);
            Assert.True(actual.WithDrive);
        }

        [Fact]
        public void AddChangeSelfDirectoryTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddChangeSelfDirectory();

            Assert.Equal(test.Actions[0], actual);
            Assert.Equal("cd /d %~dp0", actual.GetStatement());
        }

        [Fact]
        public void AddCopyTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddCopy("src", "dst");

            Assert.Equal(test.Actions[0], actual);
            Assert.Equal("src", actual.Source.Expression);
            Assert.Equal("dst", actual.Destination.Expression);
            Assert.Equal(PromptMode.Default, actual.PromptMode);
        }

        [Fact]
        public void AddEchoTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddEcho("abc");

            Assert.Equal(test.Actions[0], actual);
            Assert.Equal("abc", actual.Value.Expression);
        }

        [Fact]
        public void AddIfErrorLevelTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddIfErrorLevel(123);

            Assert.Equal(test.Actions[0], actual);
            Assert.Equal(123, actual.Level);
            Assert.False(actual.IsNot);
        }

        [Fact]
        public void AddIfExpressTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddIfExpress("LEFT", "RIGHT");

            Assert.Equal(test.Actions[0], actual);
            Assert.Equal("LEFT", actual.Left.Expression);
            Assert.Equal("RIGHT", actual.Right.Expression);
            Assert.False(actual.IsNot);
        }

        [Fact]
        public void AddIfExistTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddIfExist("path");

            Assert.Equal(test.Actions[0], actual);
            Assert.Equal("path", actual.Path.Expression);
            Assert.False(actual.IsNot);
        }

        [Fact]
        public void AddMakeDirectoryTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddMakeDirectory("abc");

            Assert.Equal(test.Actions[0], actual);
        }

        [Fact]
        public void AddPauseTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddPause();

            Assert.Equal(test.Actions[0], actual);
        }

        [Fact]
        public void AddRemarkTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddRemark("comment");

            Assert.Equal(test.Actions[0], actual);
            Assert.Equal("comment", actual.Comment.Expression);
        }


        [Fact]
        public void AddSetVariableTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddSetVariable("var", "value");

            Assert.Equal(test.Actions[0], actual);
            Assert.Equal("var", actual.VariableName);
            Assert.False(actual.IsExpress);
            Assert.False(actual.Variable.IsReadOnly);
            Assert.False(actual.Variable.DelayedExpansion);
        }

        [Fact]
        public void AddSwitchEchoTest()
        {
            var test = new CommandShellEditor();
            var actual = test.AddSwitchEcho(true);

            Assert.Equal(test.Actions[0], actual);
            Assert.True(actual.On);
        }

        #endregion

        [Fact]
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
            Assert.Equal(expected, actual);
        }

        [Fact]
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
            using(var dst = new MemoryStream()) {
                await test.WriteAsync(dst);
                var actual = unicode.GetString(dst.ToArray());
                Assert.Equal(expected, actual);
            }
        }

        #endregion
    }
}
