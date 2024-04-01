using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VsLocalizedIntellisense.Models.Logger;
using Xunit;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    public class MultiLoggerTest
    {
        #region function

        //[Fact]
        //public void Constructor_file_Test()
        //{
        //    var multiLogger = new MultiLogger(string.Empty, new MultiLogOptions() {
        //        Options = {
        //            ["file"] = new FileLogOptions()
        //            {
        //                FilePath = "NUL"
        //            }
        //        }
        //    });

        //    var po = new PrivateObject(multiLogger, "Loggers");
        //    var loggers = (IEnumerable<ILogger>)po.Target;
        //    Assert.Equal(1, loggers.Count());
        //    Assert.IsType<FileLogger>(loggers.ElementAt(0));
        //}

        //[Fact]
        //public void Constructor_debug_Test()
        //{
        //    var multiLogger = new MultiLogger(string.Empty, new MultiLogOptions() {
        //        Options = {
        //            ["debug"] = new DebugLogOptions()
        //            {
        //            }
        //        }
        //    });

        //    var po = new PrivateObject(multiLogger, "Loggers");
        //    var loggers = (IEnumerable<ILogger>)po.Target;
        //    Assert.Equal(1, loggers.Count());
        //    Assert.IsType<DebugLogger>(loggers.ElementAt(0));
        //}

        private class TestConstructor_throw: LogOptionsBase
        { }

        [Fact]
        public void Constructor_throw_Test()
        {
            Assert.Throws<NotImplementedException>(() => {
                new MultiLogger(string.Empty, new MultiLogOptions() {
                    Options = {
                        ["throw"] = new TestConstructor_throw()
                    }
                });
            });
        }

        [Theory]
        [InlineData(LogLevel.Trace)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Warning)]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Critical)]
        [InlineData(LogLevel.None)]
        public void IsEnabledTest(LogLevel level)
        {
            var logger = new MultiLogger(string.Empty, new MultiLogOptions());
            var actual = logger.IsEnabled(level);
            Assert.True(actual);
        }

        [Fact]
        public void LogTest()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH_mm_ss.fff'.log'"));

            var multiLogger = new MultiLogger(string.Empty, new MultiLogOptions() {
                Options = {
                    ["Trace"] = new FileLogOptions()
                    {
                        Level = LogLevel.Trace,
                        FilePath = path,
                    },
                    ["Debug"] = new FileLogOptions()
                    {
                        Level = LogLevel.Debug,
                        FilePath = path,
                    },
                    ["Information"] = new FileLogOptions()
                    {
                        Level = LogLevel.Information,
                        FilePath = path,
                    },
                    ["Warning"] = new FileLogOptions()
                    {
                        Level = LogLevel.Warning,
                        FilePath = path,
                    },
                    ["Error"] = new FileLogOptions()
                    {
                        Level = LogLevel.Error,
                        FilePath = path,
                    },
                    ["Critical"] = new FileLogOptions()
                    {
                        Level = LogLevel.Critical,
                        FilePath = path,
                    },
                    ["None"] = new FileLogOptions()
                    {
                        Level = LogLevel.None,
                        FilePath = path,
                    },
                }
            });

            multiLogger.LogInformation("LOG");

            multiLogger.Dispose();

            Assert.Equal(3, File.ReadAllLines(path).Length);
        }


        #endregion
    }
}
