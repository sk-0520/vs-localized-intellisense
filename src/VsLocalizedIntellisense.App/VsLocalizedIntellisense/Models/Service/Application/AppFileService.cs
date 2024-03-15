using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VsLocalizedIntellisense.Models.Configuration;
using VsLocalizedIntellisense.Models.Data;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Models.Service.Application
{
    public class AppFileService
    {
        public AppFileService(AppConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        private ILogger Logger { get; }
        private AppConfiguration Configuration { get; }

        private string CacheDirectoryPath
        {
            get => Path.Combine(Environment.ExpandEnvironmentVariables(Configuration.GetTemporaryDirectoryPath()), "cache");
        }

        public string CacheIntellisenseVersionFilePath
        {
            get => Path.Combine(CacheDirectoryPath, "intellisense-version.json");
        }

        #endregion

        #region function

        private string GetCacheIntellisenseLanguageFilePath(IntellisenseLanguageParts parts)
        {
            return Path.Combine(CacheDirectoryPath, $"language-{parts.IntellisenseVersion}-{parts.LibraryName}-{parts.Language}.json");
        }

        private void WriteJson<T>(string path, T data)
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(data.GetType());
                serializer.WriteObject(stream, data);
            }
        }

        public IntellisenseVersionData GetIntellisenseVersionData()
        {
            var cacheFile = new CacheFile<IntellisenseVersionData>(CacheIntellisenseVersionFilePath, Configuration.GetCacheTimeoutIntellisenseVersion());
            return cacheFile.Read(DateTimeOffset.Now);
        }

        public void SaveIntellisenseVersionData(IntellisenseVersionData intellisenseVersionData)
        {
            var path = CacheIntellisenseVersionFilePath;
            var dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);

            WriteJson(path, intellisenseVersionData);
        }

        public IntellisenseLanguageData GetIntellisenseLanguageData(IntellisenseLanguageParts parts)
        {
            var path = GetCacheIntellisenseLanguageFilePath(parts);
            var cacheFile = new CacheFile<IntellisenseLanguageData>(path, Configuration.GetCacheTimeoutIntellisenseLanguage());
            return cacheFile.Read(DateTimeOffset.Now);
        }

        public void SaveIntellisenseLanguageData(IntellisenseLanguageParts parts, IntellisenseLanguageData intellisenseLanguageData)
        {
            var path = GetCacheIntellisenseLanguageFilePath(parts);
            var dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);

            WriteJson(path, intellisenseLanguageData);
        }


        #endregion
    }
}
