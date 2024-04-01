using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace VsLocalizedIntellisense.Diff.Models
{
    public interface IAppPath
    {
        #region property

        public FileInfo ApplicationFile { get; }
        public DirectoryInfo ApplicationDirectory { get; }

        public DirectoryInfo RawIntellisenseDirectory { get; }
        public DirectoryInfo WorkIntellisenseDirectory { get; }

        #endregion
    }

    public class AppPath: IAppPath
    {
        public AppPath(IConfiguration configuration)
        {
            var appPath = Assembly.GetExecutingAssembly().Location;
            ApplicationFile = new FileInfo(appPath);

            RawIntellisenseDirectory = new DirectoryInfo(configuration.GetValue<string>("raw_intellisense_dir")!);
            WorkIntellisenseDirectory = new DirectoryInfo(configuration.GetValue<string>("work_intellisense_dir")!);
        }

        #region IAppPath

        public FileInfo ApplicationFile { get; }
        public DirectoryInfo ApplicationDirectory => ApplicationFile.Directory!;

        public DirectoryInfo RawIntellisenseDirectory { get; }
        public DirectoryInfo WorkIntellisenseDirectory { get; }

        #endregion
    }
}
