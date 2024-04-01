using System;
using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    public class CopyCommandTest
    {
        #region function

        [Fact]
        public void Source_throw_Test()
        {
            var test = new CopyCommand {
                Destination = "dst"
            };
            var e = Assert.Throws<InvalidOperationException>(() => test.GetStatement());
            Assert.Equal(nameof(test.Source), e.Message);
        }

        [Fact]
        public void Destination_throw_Test()
        {
            var test = new CopyCommand {
                Source = "src",
            };
            var e = Assert.Throws<InvalidOperationException>(() => test.GetStatement());
            Assert.Equal(nameof(test.Destination), e.Message);
        }

        [Fact]
        public void IsDecryptionTest()
        {
            var test = new CopyCommand {
                Source = "src",
                Destination = "dst",
            };
            Assert.Equal("copy src dst", test.GetStatement());

            test.IsDecryption = true;
            Assert.Equal("copy /d src dst", test.GetStatement());
        }

        [Fact]
        public void IsVerifyTest()
        {
            var test = new CopyCommand {
                Source = "src",
                Destination = "dst",
            };
            Assert.Equal("copy src dst", test.GetStatement());

            test.IsVerify = true;
            Assert.Equal("copy /v src dst", test.GetStatement());
        }

        [Fact]
        public void PromptModeTest()
        {
            var test = new CopyCommand {
                Source = "src",
                Destination = "dst",
            };
            Assert.Equal("copy src dst", test.GetStatement());

            test.PromptMode = PromptMode.Confirm;
            Assert.Equal("copy /-y src dst", test.GetStatement());

            test.PromptMode = PromptMode.Silent;
            Assert.Equal("copy /y src dst", test.GetStatement());

            test.PromptMode = PromptMode.Default;
            Assert.Equal("copy src dst", test.GetStatement());

            test.PromptMode = (PromptMode)(-1);
            Assert.Throws<NotImplementedException>(() => test.GetStatement());
        }

        #endregion
    }
}
