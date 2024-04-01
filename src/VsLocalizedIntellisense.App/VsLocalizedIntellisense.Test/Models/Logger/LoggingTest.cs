using System;
using System.Collections.Generic;
using VsLocalizedIntellisense.Models.Logger;
using Xunit;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    public class LoggingTest
    {
        #region function

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
            var actual = Logging.IsEnabled(defaultLevel, compareLevel);
            Assert.Equal(expected, actual);
        }

        private class LogFormatOptions: LogOptionsBase, ILogFormatOptions
        {
            public string Format { get; set; }
        }

        public class FormatTestItem
        {
            public static readonly FormatTestItem Default = new FormatTestItem(string.Empty, string.Empty) {
                Category = "FormatTest",
                Format = null,
                Level = LogLevel.Information,
                Timestamp = new DateTime(2024, 2, 25, 11, 22, 33, 123, DateTimeKind.Utc),
                CallerMemberName = "Function",
                CallerFilePath = "Z:\\directory\\📁\\a z\\NUL.cs",
                CallerLineNumber = 42,
            };

            public FormatTestItem(string expected, string message)
            {
                Expected = expected;
                Message = message;
            }

            public string Expected { get; private set; }
            public string Message { get; private set; }

            public string Category { get; set; } = Default?.Category ?? default;
            public string Format { get; set; } = Default?.Message ?? default;


            public LogLevel Level { get; set; } = Default?.Level ?? default;

            public DateTime Timestamp { get; set; } = Default?.Timestamp ?? default;

            public string CallerMemberName { get; set; } = Default?.CallerMemberName ?? default;
            public string CallerFilePath { get; set; } = Default?.CallerFilePath ?? default;
            public int CallerLineNumber { get; set; } = Default?.CallerLineNumber ?? default;
        }

        public static IEnumerable<object[]> FormatTestData => new object[][]
        {
            new []
            {
                new FormatTestItem(
                    $"2024-02-25T11:22:33.123 {FormatTestItem.Default.Level} {FormatTestItem.Default.Category} message {FormatTestItem.Default.CallerFilePath}({FormatTestItem.Default.CallerLineNumber})",
                    "message"
                ),
            }
        };

        [Theory]
        [MemberData(nameof(FormatTestData))]
        public void FormatTest(FormatTestItem item)
        {
            LogItem logItem = new LogItem(
                item.Timestamp,
                item.Level,
                item.Message,
                item.CallerMemberName,
                item.CallerFilePath,
                item.CallerLineNumber
            );
            var options = new LogFormatOptions() {
                Format = item.Format,
            };
            var actual = Logging.Format(item.Category, logItem, options);
            Assert.Equal(item.Expected, actual);
        }

        #endregion
    }
}
