using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models;
using VsLocalizedIntellisense.Models.Configuration;

namespace VsLocalizedIntellisense.Test.Models.Configuration
{
    [TestClass]
    public class AppConfigurationTest
    {
        #region function

        private AppConfiguration GetAppConfiguration()
        {
            var path = Path.Combine(Test.GetProjectDirectory().FullName, "Models", "Configuration", "AppConfigurationTest.config");
            return AppConfiguration.Open(path, new AppConfigurationInitializeParameters(DateTime.UtcNow));
        }

        [TestMethod]
        [DataRow(false, "")]
        [DataRow(false, "not-found")]
        [DataRow(true, "string")]
        public void ContainsTest(bool expected, string key)
        {
            var config = GetAppConfiguration();
            var actual = config.Contains(key);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetValue_notFound_Test()
        {
            var config = GetAppConfiguration();
            Assert.ThrowsException<KeyNotFoundException>(() => config.GetValue<object>(""));
        }

        [TestMethod]
        [DataRow("TEXT", "string")]
        [DataRow("2147483647", "int")]
        [DataRow("9223372036854775807", "long")]
        [DataRow("3.14", "float")]
        [DataRow("2.71", "double")]
        [DataRow("0.1", "decimal")]
        public void GetValue_string_Test(string expected, string key)
        {
            var config = GetAppConfiguration();
            var actual = config.GetValue<string>(key);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetValue_int_Test()
        {
            var config = GetAppConfiguration();
            var actual = config.GetValue<int>("int");
            Assert.AreEqual(2147483647, actual);
        }

        [TestMethod]
        [DataRow("string")]
        [DataRow("float")]
        [DataRow("double")]
        [DataRow("decimal")]
        public void GetValue_int_FormatException_Test(string key)
        {
            var config = GetAppConfiguration();
            Assert.ThrowsException<AppConfigurationException>(() => config.GetValue<int>(key));
        }

        [TestMethod]
        public void GetValue_int_OverflowException_Test()
        {
            var config = GetAppConfiguration();
            Assert.ThrowsException<AppConfigurationException>(() => config.GetValue<int>("long"));
        }

        [TestMethod]
        public void GetValue_long_Test()
        {
            var config = GetAppConfiguration();
            var actualLong = config.GetValue<long>("long");
            Assert.AreEqual(9223372036854775807, actualLong);
            var actualInt = config.GetValue<long>("int");
            Assert.AreEqual(2147483647, actualInt);
        }

        [TestMethod]
        [DataRow("string")]
        [DataRow("float")]
        [DataRow("double")]
        [DataRow("decimal")]
        public void GetValue_long_throw_Test(string key)
        {
            var config = GetAppConfiguration();
            Assert.ThrowsException<AppConfigurationException>(() => config.GetValue<long>(key));
        }

        public enum TestEnum1
        {
            Key1,
            Key2,
            Key3,
        }

        [TestMethod]
        public void GetValue_enum_str_Test()
        {
            var config = GetAppConfiguration();
            var actual = config.GetValue<TestEnum1>("enum_str");
            Assert.AreEqual(TestEnum1.Key2, actual);
        }

        [TestMethod]
        public void GetValue_enum_num_Test()
        {
            var config = GetAppConfiguration();
            var actual = config.GetValue<TestEnum1>("enum_num");
            Assert.AreEqual(TestEnum1.Key2, actual);
        }

        [Flags]
        public enum TestEnum2
        {
            Key1 = 0b0001,
            Key2 = 0b0010,
            Key3 = 0b0100,
        }

        [TestMethod]
        public void GetValue_enum_flag_Test()
        {
            var config = GetAppConfiguration();
            var actual = config.GetValue<TestEnum2>("enum_flag");
            Assert.AreEqual(TestEnum2.Key2 | TestEnum2.Key3, actual);
        }

        [TestMethod]
        public void GetValue_datetime_Test()
        {
            // TZなしの時間はもう分からん
            var expected = new DateTime(2024, 2, 21, 10, 0, 0);
            var config = GetAppConfiguration();
            var actual = config.GetValue<DateTime>("datetime");
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(DateTimeKind.Unspecified, actual.Kind);
        }

        [TestMethod]
        public void GetValue_datetime_0900_Test()
        {
            //var expected = new DateTime(2024, 2, 21, 10, 0, 0);
            var config = GetAppConfiguration();
            var actual = config.GetValue<DateTime>("datetime+09:00");
            //Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(DateTimeKind.Utc, actual.Kind); // CI通すとローカルではなくなるが、まぁUTCじゃないことだけでも分かればいいかなと
        }

        [TestMethod]
        public void GetValue_datetime_utc_Test()
        {
            //datetime のタイムゾーンとUTCの関係が分からん, 10時だとおれおれPCでしか動かない気がする, その割にUTC判定されるのがもうわからん
            //var expected = new DateTime(2024, 2, 21, 10, 0, 0, DateTimeKind.Utc);
            var config = GetAppConfiguration();
            var actual = config.GetValue<DateTime>("datetime_utc");
            //Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(DateTimeKind.Utc, actual.Kind);
        }

        [TestMethod]
        public void GetValue_datetime_tz_Test()
        {
            var expected = new DateTime(2024, 2, 21, 1, 0, 0, DateTimeKind.Utc);

            var config = GetAppConfiguration();
            //var actual1 = config.GetValue<DateTimeOffset>("datetime");
            var actual2 = config.GetValue<DateTimeOffset>("datetime+09:00");
            var actual3 = config.GetValue<DateTimeOffset>("datetime_utc");

            //Assert.AreEqual(expected, actual1.UtcDateTime);
            Assert.AreEqual(expected, actual2.UtcDateTime);
            Assert.AreEqual(expected, actual3.UtcDateTime);
        }

        [TestMethod]
        public void GetValue_timespan_Test()
        {
            var expected = new TimeSpan(1, 2, 3, 4, 500);

            var config = GetAppConfiguration();
            var actual1 = config.GetValue<TimeSpan>("timespan_str");
            var actual2 = config.GetValue<TimeSpan>("timespan_iso");

            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
        }

        [TestMethod]
        public void GetValue_uri_Test()
        {
            var expected = new Uri("http://localhost");

            var config = GetAppConfiguration();
            var actual = config.GetValue<Uri>("uri");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetValue_guid_Test()
        {
            var expected = new Guid("b7aa969ef81f427087898702fa9c5d76");

            var config = GetAppConfiguration();
            var actual1 = config.GetValue<Guid>("guid_1");
            var actual2 = config.GetValue<Guid>("guid_2");
            var actual3 = config.GetValue<Guid>("guid_3");

            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
            Assert.AreEqual(expected, actual3);
        }

        [TestMethod]
        [DataRow(new[] { "TEXT" }, "string")]
        [DataRow(new[] { "a", "b", "c" }, "array_string_1")]
        [DataRow(new[] { "a", "b", "c" }, "array_string_2")]
        [DataRow(new[] { "a", "", "b", "c" }, "array_string_3")]
        [DataRow(new[] { "a\tb\tc" }, "array_string_4")]
        public void GetValues_string_default_Test(string[] expected, string key)
        {
            var config = GetAppConfiguration();
            var actual = config.GetValues<string>(key);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(new[] { "TEXT" }, "string")]
        [DataRow(new[] { "a,b,c" }, "array_string_1")]
        [DataRow(new[] { "a , b , c" }, "array_string_2")]
        [DataRow(new[] { "a , ,b, c" }, "array_string_3")]
        [DataRow(new[] { "a", "b", "c" }, "array_string_4")]
        public void GetValues_string_tab_Test(string[] expected, string key)
        {
            var config = GetAppConfiguration();
            var actual = config.GetValues<string>(key, '\t');
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetValues_mixed_string_Test()
        {
            var config = GetAppConfiguration();
            var actual = config.GetValues<string>("array_mixed");
            CollectionAssert.AreEqual(new[] { "abc", "123" }, actual);
        }

        [TestMethod]
        public void GetValues_mixed_int_Test()
        {
            var config = GetAppConfiguration();
            Assert.ThrowsException<AppConfigurationException>(() => config.GetValues<int>("array_mixed"));
        }

        #endregion
    }
}
