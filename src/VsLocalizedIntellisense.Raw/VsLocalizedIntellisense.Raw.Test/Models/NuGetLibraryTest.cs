using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Raw.Models;
using Xunit;

namespace VsLocalizedIntellisense.Raw.Test.Models
{
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

        public void GetXmlDocumentEntries_empty_Test()
        {
            var test = new NuGetLibrary();
            using var archive = LoadZipArchive();

            var actual = test.GetXmlDocumentEntries(archive);

            Assert.False(actual.Any());
        }

        public void GetXmlDocumentEntries_0on1_Test()
        {
            var test = new NuGetLibrary();
            using var archive = LoadZipArchive();

            var actual = test.GetXmlDocumentEntries(archive);

            Assert.Empty(actual);
        }

        public void GetXmlDocumentEntries_1on0_Test()
        {
            var test = new NuGetLibrary();
            using var archive = LoadZipArchive();

            var actual = test.GetXmlDocumentEntries(archive);

            Assert.Empty(actual);
        }

        public void GetXmlDocumentEntries_1on1_Test()
        {
            var test = new NuGetLibrary();
            using var archive = LoadZipArchive();

            var actual = test.GetXmlDocumentEntries(archive);

            Assert.Single(actual);
            Assert.Equal("library.xml", actual.ElementAt(0).FullName);
        }

        public void GetXmlDocumentEntries_tree_Test()
        {
            var test = new NuGetLibrary();
            using var archive = LoadZipArchive();

            var actual = test.GetXmlDocumentEntries(archive);

            Assert.Empty(actual);
        }

        #endregion
    }
}
