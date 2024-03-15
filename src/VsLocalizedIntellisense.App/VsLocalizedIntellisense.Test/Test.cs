using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VsLocalizedIntellisense.Test
{
    internal class Test
    {
        static Test()
        {

        }

        public static DirectoryInfo GetProjectDirectory()
        {
            var projectTestPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)/*!*/;

            return new DirectoryInfo(projectTestPath);
        }

        public static DirectoryInfo GetClassDirectory(object test)
        {
            var project = GetProjectDirectory();
            var classTestDirPath = Path.Combine(project.FullName, test.GetType().FullName ?? throw new Exception(test.ToString()));

            return new DirectoryInfo(classTestDirPath);
        }

        public static DirectoryInfo GetMethodDirectory(object test, [CallerMemberName] string callerMemberName = "", [CallerLineNumberAttribute] int callerLineNumber = 0)
        {
            var classTestDirPath = GetClassDirectory(test);
            var methodTestDirPath = Path.Combine(classTestDirPath.FullName, callerMemberName + "-L" + callerLineNumber.ToString(CultureInfo.InvariantCulture));

            return new DirectoryInfo(methodTestDirPath);
        }

        public static void ResetDirectory(DirectoryInfo directory)
        {
            if(directory.Exists) {
                directory.Delete(true);
            }
            directory.Create();
        }

    }
}
