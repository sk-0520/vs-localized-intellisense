using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    [TestClass]
    public class LogItemTest
    {
        #region function

        [TestMethod]
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
            Assert.AreEqual(timestamp, actual.Timestamp);
            Assert.AreEqual(LogLevel.Critical, actual.Level);
            Assert.AreEqual("message", actual.Message);
            Assert.AreEqual("callerMemberName", actual.CallerMemberName);
            Assert.AreEqual("callerFilePath", actual.CallerFilePath);
            Assert.AreEqual(999, actual.CallerLineNumber);
        }

        #endregion
    }
}
