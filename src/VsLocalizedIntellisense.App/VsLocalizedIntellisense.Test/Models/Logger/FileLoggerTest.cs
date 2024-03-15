using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    [TestClass]
    public class FileLoggerTest
    {
        #region function

        [TestMethod]
        public void SingleTest()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH_mm_ss.fff'.log'"));

            Assert.IsFalse(File.Exists(path));

            var logger = new FileLogger(nameof(SingleTest), new FileLogOptions()
            {
                FilePath = path,
            });

            Assert.IsTrue(File.Exists(path));
            Assert.AreEqual(0, new FileInfo(path).Length);

            logger.LogInformation("a");
            Assert.AreNotEqual(0, new FileInfo(path).Length);

            logger.Dispose();
        }

        [TestMethod]
        public void DoubleTest()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH_mm_ss.fff'.log'"));

            Assert.IsFalse(File.Exists(path));

            var logger1 = new FileLogger(nameof(SingleTest), new FileLogOptions()
            {
                FilePath = path,
            });

            var logger2 = new FileLogger(nameof(SingleTest), new FileLogOptions()
            {
                FilePath = path,
            });

            var file = new FileInfo(path);

            file.Refresh();
            Assert.IsTrue(file.Exists);
            Assert.AreEqual(0, file.Length);

            logger1.LogInformation("a");
            file.Refresh();
            var size1 = file.Length;
            Assert.AreNotEqual(0, size1);

            logger2.LogInformation("b");
            file.Refresh();
            var size2 = file.Length;
            Assert.IsTrue(size1 < size2);

            logger1.Dispose();
            logger2.Dispose();
        }

        #endregion
    }
}
