using System;
using System.IO;
using Xunit;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    public class IOHelperTest
    {
#if !CUSTOM_ENV
        static readonly string EnvKeySystemRoot = "%SystemRoot%";
        static readonly string EnvKeyTemp = "%TEMP%";
        static readonly string EnvKeyUnknown = "%?%";
#else
        static readonly string EnvKeySystemRoot = "%CUSTOM_SystemRoot%";
        static readonly string EnvKeyTemp = "%CUSTOM_TEMP%";
        static readonly string EnvKeyUnknown = "%CUSTOM_?%";
#endif


        static readonly string EnvValueSystemRoot = Environment.ExpandEnvironmentVariables(EnvKeySystemRoot);
        static readonly string EnvValueTemp = Environment.ExpandEnvironmentVariables(EnvKeyTemp);

        #region function

        [Fact]
        public void DeleteDirectory_404_Test()
        {
            var workDir = Test.GetMethodDirectory(this);
            Test.ResetDirectory(workDir);

            var path = Path.Combine(workDir.FullName, "file");

            Assert.False(IOHelper.DeleteDirectory(path));
        }

        [Fact]
        public void DeleteDirectory_file_Test()
        {
            var workDir = Test.GetMethodDirectory(this);
            Test.ResetDirectory(workDir);

            var path = Path.Combine(workDir.FullName, "file");
            File.Create(path).Dispose();

            Assert.False(IOHelper.DeleteDirectory(path));
        }

        [Fact]
        public void DeleteDirectory_dir_Test()
        {
            var workDir = Test.GetMethodDirectory(this);
            Test.ResetDirectory(workDir);

            var path = Path.Combine(workDir.FullName, "dir");
            Directory.CreateDirectory(path);

            Assert.True(IOHelper.DeleteDirectory(path));
            Assert.False(Directory.Exists(path));
        }

        [Fact]
        public void DeleteDirectory_nest_dir_Test()
        {
            var workDir = Test.GetMethodDirectory(this);
            Test.ResetDirectory(workDir);

            var path1 = Path.Combine(workDir.FullName, "dir");
            Directory.CreateDirectory(path1);

            var path2 = Path.Combine(path1, "dir");
            Directory.CreateDirectory(path2);

            var path3 = Path.Combine(path2, "file");
            File.Create(path3).Dispose();

            Assert.True(IOHelper.DeleteDirectory(path1));
            Assert.False(Directory.Exists(path1));
        }

        [Fact]
        public void GetPhysicalDirectory_SystemRoot_Test()
        {
            var actual = IOHelper.GetPhysicalDirectory(EnvKeySystemRoot);
            Assert.Equal(new DirectoryInfo(EnvValueSystemRoot).FullName, actual.FullName);
        }


        [Fact]
        public void GetPhysicalDirectory_Temp_Test()
        {
            var actual = IOHelper.GetPhysicalDirectory(EnvKeyTemp);
            Assert.Equal(new DirectoryInfo(EnvValueTemp).FullName, actual.FullName);
        }

        [Fact]
        public void GetPhysicalDirectory_Unknown_Test()
        {
            var actual = IOHelper.GetPhysicalDirectory(EnvKeyUnknown);
            Assert.Null(actual);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GetPhysicalDirectory_Empty_Test(string input)
        {
            var actual = IOHelper.GetPhysicalDirectory(input);
            Assert.Null(actual);
        }

        #endregion
    }
}
