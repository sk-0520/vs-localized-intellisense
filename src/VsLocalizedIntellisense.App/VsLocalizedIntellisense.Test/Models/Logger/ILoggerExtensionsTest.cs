using System.Collections.Generic;
using System.Linq;
using VsLocalizedIntellisense.Models.Logger;
using Xunit;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    public class ILoggerExtensionsTest
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
        [InlineData(LogLevel.Trace, true)]
        [InlineData(LogLevel.Debug, false)]
        [InlineData(LogLevel.Information, false)]
        [InlineData(LogLevel.Warning, false)]
        [InlineData(LogLevel.Error, false)]
        [InlineData(LogLevel.Critical, false)]
        [InlineData(LogLevel.None, false)]
        public void LogTraceTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogTrace(defaultLevel.ToString());
            if(logging) {
                Assert.Equal(1, logger.Items.Count);
                Assert.Equal(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.Equal(0, logger.Items.Count);
            }
        }

        [Theory]
        [InlineData(LogLevel.Trace, true)]
        [InlineData(LogLevel.Debug, true)]
        [InlineData(LogLevel.Information, false)]
        [InlineData(LogLevel.Warning, false)]
        [InlineData(LogLevel.Error, false)]
        [InlineData(LogLevel.Critical, false)]
        [InlineData(LogLevel.None, false)]
        public void LogDebugTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogDebug(defaultLevel.ToString());
            if(logging) {
                Assert.Equal(1, logger.Items.Count);
                Assert.Equal(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.Equal(0, logger.Items.Count);
            }
        }

        [Theory]
        [InlineData(LogLevel.Trace, true)]
        [InlineData(LogLevel.Debug, true)]
        [InlineData(LogLevel.Information, true)]
        [InlineData(LogLevel.Warning, false)]
        [InlineData(LogLevel.Error, false)]
        [InlineData(LogLevel.Critical, false)]
        [InlineData(LogLevel.None, false)]
        public void LogInformationTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogInformation(defaultLevel.ToString());
            if(logging) {
                Assert.Equal(1, logger.Items.Count);
                Assert.Equal(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.Equal(0, logger.Items.Count);
            }
        }

        [Theory]
        [InlineData(LogLevel.Trace, true)]
        [InlineData(LogLevel.Debug, true)]
        [InlineData(LogLevel.Information, true)]
        [InlineData(LogLevel.Warning, true)]
        [InlineData(LogLevel.Error, false)]
        [InlineData(LogLevel.Critical, false)]
        [InlineData(LogLevel.None, false)]
        public void LogWarningTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogWarning(defaultLevel.ToString());
            if(logging) {
                Assert.Equal(1, logger.Items.Count);
                Assert.Equal(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.Equal(0, logger.Items.Count);
            }
        }

        [Theory]
        [InlineData(LogLevel.Trace, true)]
        [InlineData(LogLevel.Debug, true)]
        [InlineData(LogLevel.Information, true)]
        [InlineData(LogLevel.Warning, true)]
        [InlineData(LogLevel.Error, true)]
        [InlineData(LogLevel.Critical, false)]
        [InlineData(LogLevel.None, false)]
        public void LogErrorTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogError(defaultLevel.ToString());
            if(logging) {
                Assert.Equal(1, logger.Items.Count);
                Assert.Equal(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.Equal(0, logger.Items.Count);
            }
        }

        [Theory]
        [InlineData(LogLevel.Trace, true)]
        [InlineData(LogLevel.Debug, true)]
        [InlineData(LogLevel.Information, true)]
        [InlineData(LogLevel.Warning, true)]
        [InlineData(LogLevel.Error, true)]
        [InlineData(LogLevel.Critical, true)]
        [InlineData(LogLevel.None, false)]
        public void LogCriticalTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogCritical(defaultLevel.ToString());
            if(logging) {
                Assert.Equal(1, logger.Items.Count);
                Assert.Equal(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.Equal(0, logger.Items.Count);
            }
        }

        #endregion
    }
}
