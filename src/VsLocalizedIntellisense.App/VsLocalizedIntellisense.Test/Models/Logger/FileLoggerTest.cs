using System;
using System.IO;
using Xunit;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    public class FileLoggerTest
    {
        #region function

        [Fact]
        public void SingleTest()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH_mm_ss.fff'.log'"));

            Assert.False(File.Exists(path));

            var logger = new FileLogger(nameof(SingleTest), new FileLogOptions() {
                FilePath = path,
            });

            Assert.True(File.Exists(path));
            Assert.Equal(0, new FileInfo(path).Length);

            logger.LogInformation("a");
            Assert.NotEqual(0, new FileInfo(path).Length);

            logger.Dispose();
        }

        [Fact]
        public void DoubleTest()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH_mm_ss.fff'.log'"));

            Assert.False(File.Exists(path));

            var logger1 = new FileLogger(nameof(SingleTest), new FileLogOptions() {
                FilePath = path,
            });

            var logger2 = new FileLogger(nameof(SingleTest), new FileLogOptions() {
                FilePath = path,
            });

            var file = new FileInfo(path);

            file.Refresh();
            Assert.True(file.Exists);
            Assert.Equal(0, file.Length);

            logger1.LogInformation("a");
            file.Refresh();
            var size1 = file.Length;
            Assert.NotEqual(0, size1);

            logger2.LogInformation("b");
            file.Refresh();
            var size2 = file.Length;
            Assert.True(size1 < size2);

            logger1.Dispose();
            logger2.Dispose();
        }

        #endregion
    }
}
