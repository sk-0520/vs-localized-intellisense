using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using VsLocalizedIntellisense.Raw.Models;

namespace VsLocalizedIntellisense.Raw.Test.Models
{
    [TestClass]
    public class NuGetLibraryTest
    {
        #region function

        private DirectoryInfo GetTestClassDirectory()
        {
            var assemblyDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            var names = GetType().FullName!.Substring("VsLocalizedIntellisense.Raw.Test.".Length);
            var baseDir = names.Replace('.', Path.DirectorySeparatorChar);
            var dirPath = Path.Combine(assemblyDirectoryPath, "data", baseDir);

            return new DirectoryInfo(dirPath);
        }

        private ZipArchive LoadZipArchive([CallerMemberName] string callerMemberName = "")
        {
            var dir = GetTestClassDirectory();
            var path = Path.Combine(dir.FullName, $"{callerMemberName}.zip");

            var stream = new FileStream(path, FileMode.Open);
            var archive = new ZipArchive(stream);

            return archive;
        }

        [TestMethod]
        public void GetXmlDocumentEntries_empty_Test()
        {
            var test = new NuGetLibrary();
            using var archive = LoadZipArchive();

            var actual = test.GetXmlDocumentEntries(archive);

            Assert.IsFalse(actual.Any());
        }

        [TestMethod]
        public void GetXmlDocumentEntries_0on1_Test()
        {
            var test = new NuGetLibrary();
            using var archive = LoadZipArchive();

            var actual = test.GetXmlDocumentEntries(archive);

            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void GetXmlDocumentEntries_1on0_Test()
        {
            var test = new NuGetLibrary();
            using var archive = LoadZipArchive();

            var actual = test.GetXmlDocumentEntries(archive);

            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void GetXmlDocumentEntries_1on1_Test()
        {
            var test = new NuGetLibrary();
            using var archive = LoadZipArchive();

            var actual = test.GetXmlDocumentEntries(archive);

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual("library.xml", actual.ElementAt(0).FullName);
        }

        [TestMethod]
        public void GetXmlDocumentEntries_tree_Test()
        {
            var test = new NuGetLibrary();
            using var archive = LoadZipArchive();

            var actual = test.GetXmlDocumentEntries(archive);

            Assert.AreEqual(0, actual.Count());
        }

        #endregion
    }
}
