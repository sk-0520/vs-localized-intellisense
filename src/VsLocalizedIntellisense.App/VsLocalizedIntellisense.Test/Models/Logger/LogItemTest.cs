using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Logger;
using Xunit;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    public class LogItemTest
    {
        #region function

        [Fact]
        public void Test()
        {
            var timestamp = DateTime.Now;
            var actual = new LogItem(
                timestamp,
                LogLevel.Critical,
                "message",
                "callerMemberName",
                "callerFilePath",
                999
            );
            Assert.Equal(timestamp, actual.Timestamp);
            Assert.Equal(LogLevel.Critical, actual.Level);
            Assert.Equal("message", actual.Message);
            Assert.Equal("callerMemberName", actual.CallerMemberName);
            Assert.Equal("callerFilePath", actual.CallerFilePath);
            Assert.Equal(999, actual.CallerLineNumber);
        }

        #endregion
    }
}
