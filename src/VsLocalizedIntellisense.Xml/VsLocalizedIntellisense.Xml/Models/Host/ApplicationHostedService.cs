#if DEBUG
//#   define SHOW_DEBUG_ITEMS
#endif
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;

namespace VsLocalizedIntellisense.Xml.Models.Host
{
    public class ApplicationHostedService: IHostedService
    {
        public ApplicationHostedService(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        private IConfiguration Configuration { get; }
        private ILogger Logger { get; }

        #endregion

        #region function

        private static (string rawDirectory, string intellisenseDirectory, string dotnetVersionClr, string dotnetVersionStandard) GetRequiredOptions(IConfiguration configuration)
        {
            var rawDirectory = configuration.GetRequiredString("raw_dir");
            var intellisenseDirectory = configuration.GetRequiredString("intellisense_dir");
            var dotnetVersionClr = configuration.GetRequiredString("dotnet_version_clr");
            var dotnetVersionStandard = configuration.GetRequiredString("dotnet_version_standard");

            return (
                rawDirectory: rawDirectory,
                intellisenseDirectory: intellisenseDirectory,
                dotnetVersionClr: dotnetVersionClr,
                dotnetVersionStandard: dotnetVersionStandard
            );
        }

        private (string[] clr, string[] standard) GetLibraryOptions(IConfiguration configuration)
        {
            var librarySection = configuration.GetRequiredSection("libraries");

            var clr = librarySection.GetSectionValues<string>("clr");
            var standard = librarySection.GetSectionValues<string>("standard");

            return (
                clr: clr,
                standard: standard
            );
        }

        private IntellisenseItem GetIntellisenseItem(string rawDirectoryPath, string libraryName, string intellisenseBaseDirectoryPath)
        {
            var rawDir = new DirectoryInfo(Path.Combine(rawDirectoryPath, libraryName));
            var item = new IntellisenseItem() {
                LibraryName = rawDirectoryPath,
                RawDirectory = rawDir,
                RawFiles = rawDir.GetFiles()
            };

            var libraryDir = new DirectoryInfo(Path.Combine(intellisenseBaseDirectoryPath, libraryName));
            var dirs = libraryDir.EnumerateDirectories("*", SearchOption.TopDirectoryOnly);
            foreach(var dir in dirs) {
                var files = dir.EnumerateFiles("*.xml", SearchOption.TopDirectoryOnly).ToArray();
                item.LanguageFiles.Add(dir.Name, files);
            }

            return item;
        }

        private bool UpdateElement(XElement rawElement, XElement langElement, XNamespace intellisenseNamespace)
        {
            var isChanged = false;

            //var rawValue = new XmlName rawElement.Value

            return isChanged;
        }

        private bool UpdateElements(IReadOnlyDictionary<string, XElement> rawMemberMap, IReadOnlyDictionary<string, XElement> langMemberMap, XNamespace intellisenseNamespace)
        {
            var isChanged = false;

            foreach(var (rawName, rawElement) in rawMemberMap) {
                if(langMemberMap.TryGetValue(rawName, out var langElement)) {
                    if(UpdateElement(rawElement, langElement, intellisenseNamespace)) {
                        isChanged = true;
                    }
                }
            }

            return isChanged;
        }

        private void UpdateItem(IntellisenseItem item)
        {
            var xmlSection = Configuration.GetRequiredSection("xml");

            var xml = new {
                Namespase = xmlSection.GetSectionValue<string>("namespace"),
                Schema = xmlSection.GetSectionValue<string>("schema"),
            };
            
            foreach(var rawFile in item.RawFiles) {
                var rawXml = XDocument.Load(rawFile.FullName);
                var rawMemberMap = rawXml.GetMembers().GetMemberMap().ToFrozenDictionary();

                XNamespace intellisenseNamespace = xml.Namespase;

                foreach(var (language, files) in item.LanguageFiles) {
                    foreach(var langFile in files) {
                        if(rawFile.Name == langFile.Name) {
                            var langXml = XDocument.Load(langFile.FullName);
                            var langMemberMap = langXml.GetMembers().GetMemberMap();

                            if(UpdateElements(rawMemberMap, langMemberMap, intellisenseNamespace)) {
                                Logger.LogWarning("WRITE!");
                                langXml.Root!.Add(new XAttribute(XNamespace.Xmlns + intellisenseNamespace.NamespaceName, xml.Schema));
                                //langXml.Save(langFile.FullName + ".xml");
                            }
                        }
                    }
                }
            }
        }

        private void UpdateItems(IReadOnlyList<IntellisenseItem> items)
        {
            foreach(var item in items) {
                UpdateItem(item);
            }
        }

        #endregion

        #region IHostedService

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("hello world!");

            var requiredOptions = GetRequiredOptions(Configuration);
            var libraryOptions = GetLibraryOptions(Configuration);

            Logger.LogTrace("{requiredOptions}", requiredOptions);
            Logger.LogTrace("{libraryOptions}", libraryOptions);

            var intellisenseStandardDirectoryPath = Path.Join(requiredOptions.intellisenseDirectory, requiredOptions.dotnetVersionStandard);
            var intellisenseClrDirectoryPath = Path.Join(requiredOptions.intellisenseDirectory, requiredOptions.dotnetVersionClr);

            var items = new List<IntellisenseItem>();
            foreach(var library in libraryOptions.standard) {
                var item = GetIntellisenseItem(requiredOptions.rawDirectory, library, intellisenseStandardDirectoryPath);
                // items.Add(item);
            }
            foreach(var library in libraryOptions.clr) {
                var item = GetIntellisenseItem(requiredOptions.rawDirectory, library, intellisenseClrDirectoryPath);
                items.Add(item);
            }

#if SHOW_DEBUG_ITEMS
            foreach(var item in items) {
                Logger.LogInformation(item.LibraryName);
                foreach(var pair in item.LanguageFiles) {
                    Logger.LogInformation("[{}]", pair.Key);
                    foreach(var file in pair.Value) {
                        Logger.LogInformation("{}", file.Name);
                    }
                }
            }
#endif
            UpdateItems(items);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("bye bye!");
            return Task.CompletedTask;
        }

        #endregion
    }
}
