using System;
using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell
{
    public class CommandShellHelperTest
    {
        #region function

        [Theory]
        [InlineData("\"\"", "")]
        [InlineData("\" \"", " ")]
        [InlineData("a", "a")]
        [InlineData("\"a \"", "a ")]
        [InlineData("\"%A\"", "%A")]
        [InlineData("\"%A%\"", "%A%")]
        [InlineData("\" %A% \"", " %A% ")]
        [InlineData("^<", "<")]
        [InlineData("^>", ">")]
        [InlineData("^^", "^")]
        [InlineData("\" ^< ^^A ^> \"", " < ^A > ")]
        public void EscapeTest(string expected, string input)
        {
            var actual = CommandShellHelper.Escape(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("_", "_")]
        [InlineData("a_b", "a_b")]
        [InlineData("a_b", "a+b")]
        [InlineData("a_b", "a-b")]
        [InlineData("a_b", "a*b")]
        [InlineData("a_b", "a/b")]
        [InlineData("a_b", "a@b")]
        [InlineData("_", "あ")]
        [InlineData("A_Z", "A_Z")]
        [InlineData("0_9", "0_9")]
        public void ToSafeVariableNameTest(string expected, string input)
        {
            var actual = CommandShellHelper.ToSafeVariableName(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ToSafeVariableName_throw_Test(string input)
        {
            Assert.Throws<ArgumentException>(() => CommandShellHelper.ToSafeVariableName(input));
        }

        #endregion
    }
}
