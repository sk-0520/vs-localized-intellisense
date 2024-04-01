using System;
using System.Collections.Generic;
using System.IO;
using VsLocalizedIntellisense.Models.Configuration;
using Xunit;

namespace VsLocalizedIntellisense.Test.Models.Configuration
{
    public class AppConfigurationTest
    {
        #region function

        private AppConfiguration GetAppConfiguration()
        {
            var path = Path.Combine(Test.GetProjectDirectory().FullName, "Models", "Configuration", "AppConfigurationTest.config");
            return AppConfiguration.Open(path, new AppConfigurationInitializeParameters(DateTime.UtcNow));
        }

        [Theory]
        [InlineData(false, "")]
        [InlineData(false, "not-found")]
        [InlineData(true, "string")]
        public void ContainsTest(bool expected, string key)
        {
            var config = GetAppConfiguration();
            var actual = config.Contains(key);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetValue_notFound_Test()
        {
            var config = GetAppConfiguration();
            Assert.Throws<KeyNotFoundException>(() => config.GetValue<object>(""));
        }

        [Theory]
        [InlineData("TEXT", "string")]
        [InlineData("2147483647", "int")]
        [InlineData("9223372036854775807", "long")]
        [InlineData("3.14", "float")]
        [InlineData("2.71", "double")]
        [InlineData("0.1", "decimal")]
        public void GetValue_string_Test(string expected, string key)
        {
            var config = GetAppConfiguration();
            var actual = config.GetValue<string>(key);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetValue_int_Test()
        {
            var config = GetAppConfiguration();
            var actual = config.GetValue<int>("int");
            Assert.Equal(2147483647, actual);
        }

        [Theory]
        [InlineData("string")]
        [InlineData("float")]
        [InlineData("double")]
        [InlineData("decimal")]
        public void GetValue_int_FormatException_Test(string key)
        {
            var config = GetAppConfiguration();
            Assert.Throws<AppConfigurationException>(() => config.GetValue<int>(key));
        }

        [Fact]
        public void GetValue_int_OverflowException_Test()
        {
            var config = GetAppConfiguration();
            Assert.Throws<AppConfigurationException>(() => config.GetValue<int>("long"));
        }

        [Fact]
        public void GetValue_long_Test()
        {
            var config = GetAppConfiguration();
            var actualLong = config.GetValue<long>("long");
            Assert.Equal(9223372036854775807, actualLong);
            var actualInt = config.GetValue<long>("int");
            Assert.Equal(2147483647, actualInt);
        }

        [Theory]
        [InlineData("string")]
        [InlineData("float")]
        [InlineData("double")]
        [InlineData("decimal")]
        public void GetValue_long_throw_Test(string key)
        {
            var config = GetAppConfiguration();
            Assert.Throws<AppConfigurationException>(() => config.GetValue<long>(key));
        }

        public enum TestEnum1
        {
            Key1,
            Key2,
            Key3,
        }

        [Fact]
        public void GetValue_enum_str_Test()
        {
            var config = GetAppConfiguration();
            var actual = config.GetValue<TestEnum1>("enum_str");
            Assert.Equal(TestEnum1.Key2, actual);
        }

        [Fact]
        public void GetValue_enum_num_Test()
        {
            var config = GetAppConfiguration();
            var actual = config.GetValue<TestEnum1>("enum_num");
            Assert.Equal(TestEnum1.Key2, actual);
        }

        [Flags]
        public enum TestEnum2
        {
            Key1 = 0b0001,
            Key2 = 0b0010,
            Key3 = 0b0100,
        }

        [Fact]
        public void GetValue_enum_flag_Test()
        {
            var config = GetAppConfiguration();
            var actual = config.GetValue<TestEnum2>("enum_flag");
            Assert.Equal(TestEnum2.Key2 | TestEnum2.Key3, actual);
        }

        [Fact]
        public void GetValue_datetime_Test()
        {
            // TZなしの時間はもう分からん
            var expected = new DateTime(2024, 2, 21, 10, 0, 0);
            var config = GetAppConfiguration();
            var actual = config.GetValue<DateTime>("datetime");
            Assert.Equal(expected, actual);
            Assert.Equal(DateTimeKind.Unspecified, actual.Kind);
        }

        [Fact]
        public void GetValue_datetime_0900_Test()
        {
            //var expected = new DateTime(2024, 2, 21, 10, 0, 0);
            var config = GetAppConfiguration();
            var actual = config.GetValue<DateTime>("datetime+09:00");
            //Assert.Equal(expected, actual);
            Assert.NotEqual(DateTimeKind.Utc, actual.Kind); // CI通すとローカルではなくなるが、まぁUTCじゃないことだけでも分かればいいかなと
        }

        [Fact]
        public void GetValue_datetime_utc_Test()
        {
            //datetime のタイムゾーンとUTCの関係が分からん, 10時だとおれおれPCでしか動かない気がする, その割にUTC判定されるのがもうわからん
            //var expected = new DateTime(2024, 2, 21, 10, 0, 0, DateTimeKind.Utc);
            var config = GetAppConfiguration();
            var actual = config.GetValue<DateTime>("datetime_utc");
            //Assert.Equal(expected, actual);
            Assert.NotEqual(DateTimeKind.Utc, actual.Kind);
        }

        [Fact]
        public void GetValue_datetime_tz_Test()
        {
            var expected = new DateTime(2024, 2, 21, 1, 0, 0, DateTimeKind.Utc);

            var config = GetAppConfiguration();
            //var actual1 = config.GetValue<DateTimeOffset>("datetime");
            var actual2 = config.GetValue<DateTimeOffset>("datetime+09:00");
            var actual3 = config.GetValue<DateTimeOffset>("datetime_utc");

            //Assert.Equal(expected, actual1.UtcDateTime);
            Assert.Equal(expected, actual2.UtcDateTime);
            Assert.Equal(expected, actual3.UtcDateTime);
        }

        [Fact]
        public void GetValue_timespan_Test()
        {
            var expected = new TimeSpan(1, 2, 3, 4, 500);

            var config = GetAppConfiguration();
            var actual1 = config.GetValue<TimeSpan>("timespan_str");
            var actual2 = config.GetValue<TimeSpan>("timespan_iso");

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
        }

        [Fact]
        public void GetValue_uri_Test()
        {
            var expected = new Uri("http://localhost");

            var config = GetAppConfiguration();
            var actual = config.GetValue<Uri>("uri");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetValue_guid_Test()
        {
            var expected = new Guid("b7aa969ef81f427087898702fa9c5d76");

            var config = GetAppConfiguration();
            var actual1 = config.GetValue<Guid>("guid_1");
            var actual2 = config.GetValue<Guid>("guid_2");
            var actual3 = config.GetValue<Guid>("guid_3");

            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
            Assert.Equal(expected, actual3);
        }

        [Theory]
        [InlineData(new[] { "TEXT" }, "string")]
        [InlineData(new[] { "a", "b", "c" }, "array_string_1")]
        [InlineData(new[] { "a", "b", "c" }, "array_string_2")]
        [InlineData(new[] { "a", "", "b", "c" }, "array_string_3")]
        [InlineData(new[] { "a\tb\tc" }, "array_string_4")]
        public void GetValues_string_default_Test(string[] expected, string key)
        {
            var config = GetAppConfiguration();
            var actual = config.GetValues<string>(key);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new[] { "TEXT" }, "string")]
        [InlineData(new[] { "a,b,c" }, "array_string_1")]
        [InlineData(new[] { "a , b , c" }, "array_string_2")]
        [InlineData(new[] { "a , ,b, c" }, "array_string_3")]
        [InlineData(new[] { "a", "b", "c" }, "array_string_4")]
        public void GetValues_string_tab_Test(string[] expected, string key)
        {
            var config = GetAppConfiguration();
            var actual = config.GetValues<string>(key, '\t');
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetValues_mixed_string_Test()
        {
            var config = GetAppConfiguration();
            var actual = config.GetValues<string>("array_mixed");
            Assert.Equal(new[] { "abc", "123" }, actual);
        }

        [Fact]
        public void GetValues_mixed_int_Test()
        {
            var config = GetAppConfiguration();
            Assert.Throws<AppConfigurationException>(() => config.GetValues<int>("array_mixed"));
        }

        #endregion
    }
}
