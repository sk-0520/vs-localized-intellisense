using System.Text;
using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandPrompt.Command
{
    public class ChangeCodePageCommandTest
    {
        [Fact]
        public void Utf8_Test()
        {
            var chcp = new ChangeCodePageCommand {
                Encoding = Encoding.UTF8
            };
            var actual = chcp.GetStatement();
            Assert.Equal("chcp 65001", actual);
        }

        [Fact]
        public void ShiftJis_Test()
        {
            var chcp = new ChangeCodePageCommand() {
                Encoding = Encoding.GetEncoding("Shift_JIS"),
            };
            var actual = chcp.GetStatement();
            Assert.Equal("chcp 932", actual);
        }

        [Fact]
        public void Default_Test()
        {
            var chcp = new ChangeCodePageCommand();
            var actual = chcp.GetStatement();
            Assert.Equal($"chcp {Encoding.Default.CodePage}", actual);
        }

    }
}
