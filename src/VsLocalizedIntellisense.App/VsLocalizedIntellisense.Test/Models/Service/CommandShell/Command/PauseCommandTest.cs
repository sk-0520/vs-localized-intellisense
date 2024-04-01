using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Command;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Command
{
    public class PauseCommandTest
    {
        #region function

        [Fact]
        public void Test()
        {
            var command = new PauseCommand();
            var actual = command.GetStatement();
            Assert.Equal("pause", actual);
        }

        #endregion
    }
}
