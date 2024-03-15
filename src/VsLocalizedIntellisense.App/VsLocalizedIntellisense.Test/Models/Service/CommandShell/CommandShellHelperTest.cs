using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Service.CommandShell;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell
{
    [TestClass]
    public class CommandShellHelperTest
    {
        #region function

        [TestMethod]
        [DataRow("\"\"", "")]
        [DataRow("\" \"", " ")]
        [DataRow("a", "a")]
        [DataRow("\"a \"", "a ")]
        [DataRow("\"%A\"", "%A")]
        [DataRow("\"%A%\"", "%A%")]
        [DataRow("\" %A% \"", " %A% ")]
        [DataRow("^<", "<")]
        [DataRow("^>", ">")]
        [DataRow("^^", "^")]
        [DataRow("\" ^< ^^A ^> \"", " < ^A > ")]
        public void EscapeTest(string expected, string input)
        {
            var actual = CommandShellHelper.Escape(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("a", "a")]
        [DataRow("_", "_")]
        [DataRow("a_b", "a_b")]
        [DataRow("a_b", "a+b")]
        [DataRow("a_b", "a-b")]
        [DataRow("a_b", "a*b")]
        [DataRow("a_b", "a/b")]
        [DataRow("a_b", "a@b")]
        [DataRow("_", "あ")]
        [DataRow("A_Z", "A_Z")]
        [DataRow("0_9", "0_9")]
        public void ToSafeVariableNameTest(string expected, string input)
        {
            var actual = CommandShellHelper.ToSafeVariableName(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void ToSafeVariableName_throw_Test(string input)
        {
            Assert.ThrowsException<ArgumentException>(() => CommandShellHelper.ToSafeVariableName(input));
        }

        #endregion
    }
}
