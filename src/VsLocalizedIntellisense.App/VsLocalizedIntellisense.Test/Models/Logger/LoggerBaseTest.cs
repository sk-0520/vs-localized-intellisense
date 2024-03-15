using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    [TestClass]
    public class LoggerBaseTest
    {
        #region function

        private class TestLogger : LoggerBase<TestLogger.TestLogOptions>
        {
            internal class TestLogOptions : LogOptionsBase
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
        // Trace
        [DataRow(true, LogLevel.Trace, LogLevel.Trace)]
        [DataRow(true, LogLevel.Trace, LogLevel.Debug)]
        [DataRow(true, LogLevel.Trace, LogLevel.Information)]
        [DataRow(true, LogLevel.Trace, LogLevel.Warning)]
        [DataRow(true, LogLevel.Trace, LogLevel.Error)]
        [DataRow(true, LogLevel.Trace, LogLevel.Critical)]
        [DataRow(false, LogLevel.Trace, LogLevel.None)]
        // Debug
        [DataRow(false, LogLevel.Debug, LogLevel.Trace)]
        [DataRow(true, LogLevel.Debug, LogLevel.Debug)]
        [DataRow(true, LogLevel.Debug, LogLevel.Information)]
        [DataRow(true, LogLevel.Debug, LogLevel.Warning)]
        [DataRow(true, LogLevel.Debug, LogLevel.Error)]
        [DataRow(true, LogLevel.Debug, LogLevel.Critical)]
        [DataRow(false, LogLevel.Debug, LogLevel.None)]
        // Information
        [DataRow(false, LogLevel.Information, LogLevel.Trace)]
        [DataRow(false, LogLevel.Information, LogLevel.Debug)]
        [DataRow(true, LogLevel.Information, LogLevel.Information)]
        [DataRow(true, LogLevel.Information, LogLevel.Warning)]
        [DataRow(true, LogLevel.Information, LogLevel.Error)]
        [DataRow(true, LogLevel.Information, LogLevel.Critical)]
        [DataRow(false, LogLevel.Information, LogLevel.None)]
        // Warning
        [DataRow(false, LogLevel.Warning, LogLevel.Trace)]
        [DataRow(false, LogLevel.Warning, LogLevel.Debug)]
        [DataRow(false, LogLevel.Warning, LogLevel.Information)]
        [DataRow(true, LogLevel.Warning, LogLevel.Warning)]
        [DataRow(true, LogLevel.Warning, LogLevel.Error)]
        [DataRow(true, LogLevel.Warning, LogLevel.Critical)]
        [DataRow(false, LogLevel.Warning, LogLevel.None)]
        // Error
        [DataRow(false, LogLevel.Error, LogLevel.Trace)]
        [DataRow(false, LogLevel.Error, LogLevel.Debug)]
        [DataRow(false, LogLevel.Error, LogLevel.Information)]
        [DataRow(false, LogLevel.Error, LogLevel.Warning)]
        [DataRow(true, LogLevel.Error, LogLevel.Error)]
        [DataRow(true, LogLevel.Error, LogLevel.Critical)]
        [DataRow(false, LogLevel.Error, LogLevel.None)]
        // Critical
        [DataRow(false, LogLevel.Critical, LogLevel.Trace)]
        [DataRow(false, LogLevel.Critical, LogLevel.Debug)]
        [DataRow(false, LogLevel.Critical, LogLevel.Information)]
        [DataRow(false, LogLevel.Critical, LogLevel.Warning)]
        [DataRow(false, LogLevel.Critical, LogLevel.Error)]
        [DataRow(true, LogLevel.Critical, LogLevel.Critical)]
        [DataRow(false, LogLevel.Critical, LogLevel.None)]
        // None
        [DataRow(false, LogLevel.None, LogLevel.Trace)]
        [DataRow(false, LogLevel.None, LogLevel.Debug)]
        [DataRow(false, LogLevel.None, LogLevel.Information)]
        [DataRow(false, LogLevel.None, LogLevel.Warning)]
        [DataRow(false, LogLevel.None, LogLevel.Error)]
        [DataRow(false, LogLevel.None, LogLevel.Critical)]
        [DataRow(false, LogLevel.None, LogLevel.None)]
        public void IsEnabledTest(bool expected, LogLevel defaultLevel, LogLevel compareLevel)
        {
            var logger = new TestLogger(defaultLevel);
            var actual = logger.IsEnabled(compareLevel);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // Trace
        [DataRow(LogLevel.Trace, LogLevel.Trace, true)]
        [DataRow(LogLevel.Trace, LogLevel.Debug, true)]
        [DataRow(LogLevel.Trace, LogLevel.Information, true)]
        [DataRow(LogLevel.Trace, LogLevel.Warning, true)]
        [DataRow(LogLevel.Trace, LogLevel.Error, true)]
        [DataRow(LogLevel.Trace, LogLevel.Critical, true)]
        [DataRow(LogLevel.Trace, LogLevel.None, false)]
        // Debug
        [DataRow(LogLevel.Debug, LogLevel.Trace, false)]
        [DataRow(LogLevel.Debug, LogLevel.Debug, true)]
        [DataRow(LogLevel.Debug, LogLevel.Information, true)]
        [DataRow(LogLevel.Debug, LogLevel.Warning, true)]
        [DataRow(LogLevel.Debug, LogLevel.Error, true)]
        [DataRow(LogLevel.Debug, LogLevel.Critical, true)]
        [DataRow(LogLevel.Debug, LogLevel.None, false)]
        // Information
        [DataRow(LogLevel.Information, LogLevel.Trace, false)]
        [DataRow(LogLevel.Information, LogLevel.Debug, false)]
        [DataRow(LogLevel.Information, LogLevel.Information, true)]
        [DataRow(LogLevel.Information, LogLevel.Warning, true)]
        [DataRow(LogLevel.Information, LogLevel.Error, true)]
        [DataRow(LogLevel.Information, LogLevel.Critical, true)]
        [DataRow(LogLevel.Information, LogLevel.None, false)]
        // Warning
        [DataRow(LogLevel.Warning, LogLevel.Trace, false)]
        [DataRow(LogLevel.Warning, LogLevel.Debug, false)]
        [DataRow(LogLevel.Warning, LogLevel.Information, false)]
        [DataRow(LogLevel.Warning, LogLevel.Warning, true)]
        [DataRow(LogLevel.Warning, LogLevel.Error, true)]
        [DataRow(LogLevel.Warning, LogLevel.Critical, true)]
        [DataRow(LogLevel.Warning, LogLevel.None, false)]
        // Error
        [DataRow(LogLevel.Error, LogLevel.Trace, false)]
        [DataRow(LogLevel.Error, LogLevel.Debug, false)]
        [DataRow(LogLevel.Error, LogLevel.Information, false)]
        [DataRow(LogLevel.Error, LogLevel.Warning, false)]
        [DataRow(LogLevel.Error, LogLevel.Error, true)]
        [DataRow(LogLevel.Error, LogLevel.Critical, true)]
        [DataRow(LogLevel.Error, LogLevel.None, false)]
        // Critical
        [DataRow(LogLevel.Critical, LogLevel.Trace, false)]
        [DataRow(LogLevel.Critical, LogLevel.Debug, false)]
        [DataRow(LogLevel.Critical, LogLevel.Information, false)]
        [DataRow(LogLevel.Critical, LogLevel.Warning, false)]
        [DataRow(LogLevel.Critical, LogLevel.Error, false)]
        [DataRow(LogLevel.Critical, LogLevel.Critical, true)]
        [DataRow(LogLevel.Critical, LogLevel.None, false)]
        // None
        [DataRow(LogLevel.None, LogLevel.Trace, false)]
        [DataRow(LogLevel.None, LogLevel.Debug, false)]
        [DataRow(LogLevel.None, LogLevel.Information, false)]
        [DataRow(LogLevel.None, LogLevel.Warning, false)]
        [DataRow(LogLevel.None, LogLevel.Error, false)]
        [DataRow(LogLevel.None, LogLevel.Critical, false)]
        [DataRow(LogLevel.None, LogLevel.None, false)]
        public void LogTest(LogLevel defaultLevel, LogLevel level, bool logging)
        {
            var logger = new TestLogger(defaultLevel);
            logger.Log(level, level.ToString());
            if (logging)
            {
                Assert.AreEqual(1, logger.Items.Count);
                Assert.AreEqual(level.ToString(), logger.Items.Last().Message);
            }
            else
            {
                Assert.AreEqual(0, logger.Items.Count);
            }
        }

        #endregion
    }
}
