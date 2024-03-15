using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    [TestClass]
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

        [TestMethod]
        [DataRow(LogLevel.Trace, true)]
        [DataRow(LogLevel.Debug, false)]
        [DataRow(LogLevel.Information, false)]
        [DataRow(LogLevel.Warning, false)]
        [DataRow(LogLevel.Error, false)]
        [DataRow(LogLevel.Critical, false)]
        [DataRow(LogLevel.None, false)]
        public void LogTraceTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogTrace(defaultLevel.ToString());
            if(logging) {
                Assert.AreEqual(1, logger.Items.Count);
                Assert.AreEqual(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.AreEqual(0, logger.Items.Count);
            }
        }

        [TestMethod]
        [DataRow(LogLevel.Trace, true)]
        [DataRow(LogLevel.Debug, true)]
        [DataRow(LogLevel.Information, false)]
        [DataRow(LogLevel.Warning, false)]
        [DataRow(LogLevel.Error, false)]
        [DataRow(LogLevel.Critical, false)]
        [DataRow(LogLevel.None, false)]
        public void LogDebugTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogDebug(defaultLevel.ToString());
            if(logging) {
                Assert.AreEqual(1, logger.Items.Count);
                Assert.AreEqual(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.AreEqual(0, logger.Items.Count);
            }
        }

        [TestMethod]
        [DataRow(LogLevel.Trace, true)]
        [DataRow(LogLevel.Debug, true)]
        [DataRow(LogLevel.Information, true)]
        [DataRow(LogLevel.Warning, false)]
        [DataRow(LogLevel.Error, false)]
        [DataRow(LogLevel.Critical, false)]
        [DataRow(LogLevel.None, false)]
        public void LogInformationTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogInformation(defaultLevel.ToString());
            if(logging) {
                Assert.AreEqual(1, logger.Items.Count);
                Assert.AreEqual(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.AreEqual(0, logger.Items.Count);
            }
        }

        [TestMethod]
        [DataRow(LogLevel.Trace, true)]
        [DataRow(LogLevel.Debug, true)]
        [DataRow(LogLevel.Information, true)]
        [DataRow(LogLevel.Warning, true)]
        [DataRow(LogLevel.Error, false)]
        [DataRow(LogLevel.Critical, false)]
        [DataRow(LogLevel.None, false)]
        public void LogWarningTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogWarning(defaultLevel.ToString());
            if(logging) {
                Assert.AreEqual(1, logger.Items.Count);
                Assert.AreEqual(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.AreEqual(0, logger.Items.Count);
            }
        }

        [TestMethod]
        [DataRow(LogLevel.Trace, true)]
        [DataRow(LogLevel.Debug, true)]
        [DataRow(LogLevel.Information, true)]
        [DataRow(LogLevel.Warning, true)]
        [DataRow(LogLevel.Error, true)]
        [DataRow(LogLevel.Critical, false)]
        [DataRow(LogLevel.None, false)]
        public void LogErrorTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogError(defaultLevel.ToString());
            if(logging) {
                Assert.AreEqual(1, logger.Items.Count);
                Assert.AreEqual(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.AreEqual(0, logger.Items.Count);
            }
        }

        [TestMethod]
        [DataRow(LogLevel.Trace, true)]
        [DataRow(LogLevel.Debug, true)]
        [DataRow(LogLevel.Information, true)]
        [DataRow(LogLevel.Warning, true)]
        [DataRow(LogLevel.Error, true)]
        [DataRow(LogLevel.Critical, true)]
        [DataRow(LogLevel.None, false)]
        public void LogCriticalTest(LogLevel defaultLevel, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.LogCritical(defaultLevel.ToString());
            if(logging) {
                Assert.AreEqual(1, logger.Items.Count);
                Assert.AreEqual(defaultLevel.ToString(), logger.Items.Last().Message);
            } else {
                Assert.AreEqual(0, logger.Items.Count);
            }
        }

        #endregion
    }
}
