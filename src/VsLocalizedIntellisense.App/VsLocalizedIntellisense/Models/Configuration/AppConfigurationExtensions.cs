using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Models.Configuration
{
    public static class AppConfigurationExtensions
    {
        #region function

        private static TResult GetValueOrEmptyInit<TResult>(AppConfiguration configuration, string key)
        {
            return configuration.Contains(key) ? configuration.GetValue<TResult>(key) : default;
        }

        public static string GetHttpUserAgent(this AppConfiguration configuration)
        {
            var rawHttpUserAgent = configuration.GetValue<string>("http-user-agent");
            return configuration.Replace(rawHttpUserAgent);
        }

        /// <summary>
        /// アップデートチェック用URIを取得。
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Uri GetUpdateCheckUri(this AppConfiguration configuration) => configuration.GetValue<Uri>("update-check-uri");

        public static Uri GetReleaseUri(this AppConfiguration configuration) => configuration.GetValue<Uri>("update-release-uri");

        public static string GetInstallRootDirectoryPath(this AppConfiguration configuration) => configuration.GetValue<string>("install-root-directory");

        public static string GetTemporaryDirectoryPath(this AppConfiguration configuration) => configuration.Replace(configuration.GetValue<string>("temp-directory-path"));
        public static string GetWorkingDirectoryPath(this AppConfiguration configuration) => configuration.Replace(configuration.GetValue<string>("work-directory-path"));

        public static TimeSpan GetCacheTimeoutIntellisenseVersion(this AppConfiguration configuration) => configuration.GetValue<TimeSpan>("cache-timeout-intellisense-version");
        public static TimeSpan GetCacheTimeoutIntellisenseLanguage(this AppConfiguration configuration) => configuration.GetValue<TimeSpan>("cache-timeout-intellisense-language");
        


        public static string[] GetIntellisenseDirectories(this AppConfiguration configuration) => configuration.GetValues<string>("intellisense-directories", '|');

        public static string[] GetIntellisenseDotNetStandardMappings(this AppConfiguration configuration) => configuration.GetValues<string>("intellisense-dotnet-standard-mappings", '|');
        public static Regex GetIntellisenseDotNetStandardVersion(this AppConfiguration configuration) => new Regex(configuration.GetValue<string>("intellisense-dotnet-standard-version"));
        public static string[] GetIntellisenseDotNetRuntimeMappings(this AppConfiguration configuration) => configuration.GetValues<string>("intellisense-dotnet-runtime-mappings", '|');
        public static Regex GetIntellisenseDotNetRuntimeVersion(this AppConfiguration configuration) => new Regex(configuration.GetValue<string>("intellisense-dotnet-runtime-version"));

        public static string[] GetLanguageItems(this AppConfiguration configuration) => configuration.GetValues<string>("language-items");

        public static string GetRepositoryRevision(this AppConfiguration configuration) => configuration.GetValue<string>("repository-revision");
        public static string GetRepositoryOwner(this AppConfiguration configuration) => configuration.GetValue<string>("repository-owner");
        public static string GetRepositoryName(this AppConfiguration configuration) => configuration.GetValue<string>("repository-name");

        public static LogLevel GetLogDefaultLevel(this AppConfiguration configuration) => configuration.GetValue<LogLevel>("log-default-level");

        public static bool IsEnableDebugLog(this AppConfiguration configuration)
        {
            return GetValueOrEmptyInit<bool>(configuration, "log-debug-is-enabled");
        }
        public static string GetLogDebugFormat(this AppConfiguration configuration)
        {
            return GetValueOrEmptyInit<string>(configuration, "log-debug-format");
        }

        public static bool IsEnableTraceLog(this AppConfiguration configuration)
        {
            return GetValueOrEmptyInit<bool>(configuration, "log-trace-is-enabled");
        }
        public static string GetLogTraceFormat(this AppConfiguration configuration)
        {
            return GetValueOrEmptyInit<string>(configuration, "log-trace-format");
        }

        public static bool IsEnableFileLog(this AppConfiguration configuration)
        {
            return GetValueOrEmptyInit<bool>(configuration, "log-file-is-enabled");
        }
        public static string GetLogFileFormat(this AppConfiguration configuration)
        {
            return GetValueOrEmptyInit<string>(configuration, "log-file-format");
        }
        public static string GetLogFilePath(this AppConfiguration configuration)
        {
            var value = configuration.GetValue<string>("log-file-path");
            return configuration.Replace(value);
        }


        #endregion
    }
}
