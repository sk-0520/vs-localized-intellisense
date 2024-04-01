using System.Collections.Generic;
using System.Linq;
using VsLocalizedIntellisense.Models.Logger;
using Xunit;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    public class LoggerBaseTest
    {
        #region function

        private class TestLogger: LoggerBase<TestLogger.TestLogOptions>
        {
            internal class TestLogOptions: LogOptionsBase
            {
                public TestLogOptions(LogLevel level)
                {
                    Level = level;
                }
            }

            public TestLogger(LogLevel logLevel)
                : base(string.Empty, new TestLogOptions(logLevel))
            { }

            public IList<LogItem> Items { get; } = new List<LogItem>();

            public override void OutputLog(in LogItem logItem)
            {
                Items.Add(logItem);
            }
        }

        [Theory]
        // Trace
        [InlineData(true, LogLevel.Trace, LogLevel.Trace)]
        [InlineData(true, LogLevel.Trace, LogLevel.Debug)]
        [InlineData(true, LogLevel.Trace, LogLevel.Information)]
        [InlineData(true, LogLevel.Trace, LogLevel.Warning)]
        [InlineData(true, LogLevel.Trace, LogLevel.Error)]
        [InlineData(true, LogLevel.Trace, LogLevel.Critical)]
        [InlineData(false, LogLevel.Trace, LogLevel.None)]
        // Debug
        [InlineData(false, LogLevel.Debug, LogLevel.Trace)]
        [InlineData(true, LogLevel.Debug, LogLevel.Debug)]
        [InlineData(true, LogLevel.Debug, LogLevel.Information)]
        [InlineData(true, LogLevel.Debug, LogLevel.Warning)]
        [InlineData(true, LogLevel.Debug, LogLevel.Error)]
        [InlineData(true, LogLevel.Debug, LogLevel.Critical)]
        [InlineData(false, LogLevel.Debug, LogLevel.None)]
        // Information
        [InlineData(false, LogLevel.Information, LogLevel.Trace)]
        [InlineData(false, LogLevel.Information, LogLevel.Debug)]
        [InlineData(true, LogLevel.Information, LogLevel.Information)]
        [InlineData(true, LogLevel.Information, LogLevel.Warning)]
        [InlineData(true, LogLevel.Information, LogLevel.Error)]
        [InlineData(true, LogLevel.Information, LogLevel.Critical)]
        [InlineData(false, LogLevel.Information, LogLevel.None)]
        // Warning
        [InlineData(false, LogLevel.Warning, LogLevel.Trace)]
        [InlineData(false, LogLevel.Warning, LogLevel.Debug)]
        [InlineData(false, LogLevel.Warning, LogLevel.Information)]
        [InlineData(true, LogLevel.Warning, LogLevel.Warning)]
        [InlineData(true, LogLevel.Warning, LogLevel.Error)]
        [InlineData(true, LogLevel.Warning, LogLevel.Critical)]
        [InlineData(false, LogLevel.Warning, LogLevel.None)]
        // Error
        [InlineData(false, LogLevel.Error, LogLevel.Trace)]
        [InlineData(false, LogLevel.Error, LogLevel.Debug)]
        [InlineData(false, LogLevel.Error, LogLevel.Information)]
        [InlineData(false, LogLevel.Error, LogLevel.Warning)]
        [InlineData(true, LogLevel.Error, LogLevel.Error)]
        [InlineData(true, LogLevel.Error, LogLevel.Critical)]
        [InlineData(false, LogLevel.Error, LogLevel.None)]
        // Critical
        [InlineData(false, LogLevel.Critical, LogLevel.Trace)]
        [InlineData(false, LogLevel.Critical, LogLevel.Debug)]
        [InlineData(false, LogLevel.Critical, LogLevel.Information)]
        [InlineData(false, LogLevel.Critical, LogLevel.Warning)]
        [InlineData(false, LogLevel.Critical, LogLevel.Error)]
        [InlineData(true, LogLevel.Critical, LogLevel.Critical)]
        [InlineData(false, LogLevel.Critical, LogLevel.None)]
        // None
        [InlineData(false, LogLevel.None, LogLevel.Trace)]
        [InlineData(false, LogLevel.None, LogLevel.Debug)]
        [InlineData(false, LogLevel.None, LogLevel.Information)]
        [InlineData(false, LogLevel.None, LogLevel.Warning)]
        [InlineData(false, LogLevel.None, LogLevel.Error)]
        [InlineData(false, LogLevel.None, LogLevel.Critical)]
        [InlineData(false, LogLevel.None, LogLevel.None)]
        public void IsEnabledTest(bool expected, LogLevel defaultLevel, LogLevel compareLevel)
        {
            var logger = new TestLogger(defaultLevel);
            var actual = logger.IsEnabled(compareLevel);
            Assert.Equal(expected, actual);
        }

        [Theory]
        // Trace
        [InlineData(LogLevel.Trace, LogLevel.Trace, true)]
        [InlineData(LogLevel.Trace, LogLevel.Debug, true)]
        [InlineData(LogLevel.Trace, LogLevel.Information, true)]
        [InlineData(LogLevel.Trace, LogLevel.Warning, true)]
        [InlineData(LogLevel.Trace, LogLevel.Error, true)]
        [InlineData(LogLevel.Trace, LogLevel.Critical, true)]
        [InlineData(LogLevel.Trace, LogLevel.None, false)]
        // Debug
        [InlineData(LogLevel.Debug, LogLevel.Trace, false)]
        [InlineData(LogLevel.Debug, LogLevel.Debug, true)]
        [InlineData(LogLevel.Debug, LogLevel.Information, true)]
        [InlineData(LogLevel.Debug, LogLevel.Warning, true)]
        [InlineData(LogLevel.Debug, LogLevel.Error, true)]
        [InlineData(LogLevel.Debug, LogLevel.Critical, true)]
        [InlineData(LogLevel.Debug, LogLevel.None, false)]
        // Information
        [InlineData(LogLevel.Information, LogLevel.Trace, false)]
        [InlineData(LogLevel.Information, LogLevel.Debug, false)]
        [InlineData(LogLevel.Information, LogLevel.Information, true)]
        [InlineData(LogLevel.Information, LogLevel.Warning, true)]
        [InlineData(LogLevel.Information, LogLevel.Error, true)]
        [InlineData(LogLevel.Information, LogLevel.Critical, true)]
        [InlineData(LogLevel.Information, LogLevel.None, false)]
        // Warning
        [InlineData(LogLevel.Warning, LogLevel.Trace, false)]
        [InlineData(LogLevel.Warning, LogLevel.Debug, false)]
        [InlineData(LogLevel.Warning, LogLevel.Information, false)]
        [InlineData(LogLevel.Warning, LogLevel.Warning, true)]
        [InlineData(LogLevel.Warning, LogLevel.Error, true)]
        [InlineData(LogLevel.Warning, LogLevel.Critical, true)]
        [InlineData(LogLevel.Warning, LogLevel.None, false)]
        // Error
        [InlineData(LogLevel.Error, LogLevel.Trace, false)]
        [InlineData(LogLevel.Error, LogLevel.Debug, false)]
        [InlineData(LogLevel.Error, LogLevel.Information, false)]
        [InlineData(LogLevel.Error, LogLevel.Warning, false)]
        [InlineData(LogLevel.Error, LogLevel.Error, true)]
        [InlineData(LogLevel.Error, LogLevel.Critical, true)]
        [InlineData(LogLevel.Error, LogLevel.None, false)]
        // Critical
        [InlineData(LogLevel.Critical, LogLevel.Trace, false)]
        [InlineData(LogLevel.Critical, LogLevel.Debug, false)]
        [InlineData(LogLevel.Critical, LogLevel.Information, false)]
        [InlineData(LogLevel.Critical, LogLevel.Warning, false)]
        [InlineData(LogLevel.Critical, LogLevel.Error, false)]
        [InlineData(LogLevel.Critical, LogLevel.Critical, true)]
        [InlineData(LogLevel.Critical, LogLevel.None, false)]
        // None
        [InlineData(LogLevel.None, LogLevel.Trace, false)]
        [InlineData(LogLevel.None, LogLevel.Debug, false)]
        [InlineData(LogLevel.None, LogLevel.Information, false)]
        [InlineData(LogLevel.None, LogLevel.Warning, false)]
        [InlineData(LogLevel.None, LogLevel.Error, false)]
        [InlineData(LogLevel.None, LogLevel.Critical, false)]
        [InlineData(LogLevel.None, LogLevel.None, false)]
        public void LogTest(LogLevel defaultLevel, LogLevel level, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.Log(level, level.ToString());
            if(logging) {
                Assert.Equal(1, logger.Items.Count);
                Assert.Equal(level.ToString(), logger.Items.Last().Message);
            } else {
                Assert.Equal(0, logger.Items.Count);
            }
        }

        #endregion
    }
}
