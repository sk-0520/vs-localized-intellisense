using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    [TestClass]
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

        [TestMethod]
        public void DeleteDirectory_404_Test()
        {
            var workDir = Test.GetMethodDirectory(this);
            Test.ResetDirectory(workDir);

            var path = Path.Combine(workDir.FullName, "file");

            Assert.IsFalse(IOHelper.DeleteDirectory(path));
        }

        [TestMethod]
        public void DeleteDirectory_file_Test()
        {
            var workDir = Test.GetMethodDirectory(this);
            Test.ResetDirectory(workDir);

            var path = Path.Combine(workDir.FullName, "file");
            File.Create(path).Dispose();

            Assert.IsFalse(IOHelper.DeleteDirectory(path));
        }

        [TestMethod]
        public void DeleteDirectory_dir_Test()
        {
            var workDir = Test.GetMethodDirectory(this);
            Test.ResetDirectory(workDir);

            var path = Path.Combine(workDir.FullName, "dir");
            Directory.CreateDirectory(path);

            Assert.IsTrue(IOHelper.DeleteDirectory(path));
            Assert.IsFalse(Directory.Exists(path));
        }

        [TestMethod]
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

            Assert.IsTrue(IOHelper.DeleteDirectory(path1));
            Assert.IsFalse(Directory.Exists(path1));
        }

        [TestMethod]
        public void GetPhysicalDirectory_SystemRoot_Test()
        {
            var actual = IOHelper.GetPhysicalDirectory(EnvKeySystemRoot);
            Assert.AreEqual(new DirectoryInfo(EnvValueSystemRoot).FullName, actual.FullName);
        }


        [TestMethod]
        public void GetPhysicalDirectory_Temp_Test()
        {
            var actual = IOHelper.GetPhysicalDirectory(EnvKeyTemp);
            Assert.AreEqual(new DirectoryInfo(EnvValueTemp).FullName, actual.FullName);
        }

        [TestMethod]
        public void GetPhysicalDirectory_Unknown_Test()
        {
            var actual = IOHelper.GetPhysicalDirectory(EnvKeyUnknown);
            Assert.IsNull(actual);
        }


        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void GetPhysicalDirectory_Empty_Test(string input)
        {
            var actual = IOHelper.GetPhysicalDirectory(input);
            Assert.IsNull(actual);
        }

        #endregion
    }
}
