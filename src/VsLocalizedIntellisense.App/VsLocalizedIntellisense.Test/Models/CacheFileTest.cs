using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xunit;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    public class CacheFileTest
    {
        #region define

        [DataContract]
        private class Data: ICachedTimestamp
        {
            [DataMember]
            public DateTimeOffset CachedTimestamp { get; set; }
            [DataMember]
            public string Value { get; set; }
        }

        #endregion

        #region function

        private void Write<T>(string path, T data)
        {
            using(var stream = new FileStream(path, FileMode.Create)) {
                var serializer = new DataContractJsonSerializer(data.GetType());
                serializer.WriteObject(stream, data);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_throw_Test(string path)
        {
            var span = TimeSpan.FromSeconds(10);
            Assert.Throws<ArgumentException>(() => new CacheFile<Data>(path, span));
        }

        [Fact]
        public void Read_notFound_Test()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, nameof(Read_notFound_Test));
            var span = TimeSpan.FromSeconds(10);
            var currentTimestamp = DateTimeOffset.Now;

            var cf = new CacheFile<Data>(path, span);
            var actual = cf.Read(currentTimestamp);
            Assert.Null(actual);
        }

        [Fact]
        public void Read_empty1_Test()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, nameof(Read_empty1_Test));
            var span = TimeSpan.FromSeconds(10);
            var currentTimestamp = DateTimeOffset.Now;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Create(path).Dispose();

            var cf = new CacheFile<Data>(path, span);
            var actual = cf.Read(currentTimestamp);
            Assert.Null(actual);
        }

        [Fact]
        public void Read_empty2_Test()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, nameof(Read_empty2_Test));
            var span = TimeSpan.FromSeconds(10);
            var currentTimestamp = DateTimeOffset.Now;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using(var stream = File.CreateText(path)) {
                stream.Write("{}");
            }

            var cf = new CacheFile<Data>(path, span);
            var actual = cf.Read(currentTimestamp);
            Assert.Null(actual);
        }

        [Fact]
        public void Read_EnabledTime_Test()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, nameof(Read_EnabledTime_Test));
            var span = TimeSpan.FromSeconds(10);
            var cacheTimestamp = DateTimeOffset.Now;
            var currentTimestamp = cacheTimestamp + span - TimeSpan.FromMilliseconds(1);

            dir.Create();
            var src = new Data() {
                CachedTimestamp = cacheTimestamp,
                Value = nameof(Read_EnabledTime_Test),
            };
            Write(path, src);

            var cf = new CacheFile<Data>(path, span);

            var actual = cf.Read(currentTimestamp);
            Assert.NotNull(actual);
            Assert.Equal(nameof(Read_EnabledTime_Test), actual.Value);
        }

        [Fact]
        public void Read_DisableTime_Test()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, nameof(Read_EnabledTime_Test));
            var span = TimeSpan.FromSeconds(10);
            var cacheTimestamp = DateTimeOffset.Now;
            var currentTimestamp = cacheTimestamp + span;

            dir.Create();
            var src = new Data() {
                CachedTimestamp = cacheTimestamp,
                Value = nameof(Read_EnabledTime_Test),
            };
            Write(path, src);

            var cf = new CacheFile<Data>(path, span);

            var actual = cf.Read(currentTimestamp);
            Assert.Null(actual);
        }

        [Fact]
        public void Read_old_Test()
        {
            var dir = Test.GetMethodDirectory(this);
            var path = Path.Combine(dir.FullName, nameof(Read_EnabledTime_Test));
            var span = TimeSpan.FromSeconds(10);
            var cacheTimestamp = DateTimeOffset.Now;
            var currentTimestamp = cacheTimestamp - span;

            dir.Create();
            var src = new Data() {
                CachedTimestamp = cacheTimestamp,
                Value = nameof(Read_old_Test),
            };
            Write(path, src);

            var cf = new CacheFile<Data>(path, span);

            var actual = cf.Read(currentTimestamp);
            Assert.NotNull(actual);
            Assert.Equal(nameof(Read_old_Test), actual.Value);
        }

        #endregion
    }
}
