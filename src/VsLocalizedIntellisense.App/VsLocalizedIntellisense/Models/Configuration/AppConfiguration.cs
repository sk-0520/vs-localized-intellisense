using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Configuration.Internal;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;

namespace VsLocalizedIntellisense.Models.Configuration
{

    [Serializable]
    public class AppConfigurationException : Exception
    {
        public AppConfigurationException() { }
        public AppConfigurationException(string message) : base(message) { }
        public AppConfigurationException(string message, Exception inner) : base(message, inner) { }
        protected AppConfigurationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// App.config のなんかそれっぽいの。
    /// <para><see cref="ConfigurationManager"/>は直接使わない方針。</para>
    /// </summary>
    public sealed class AppConfiguration
    {
        /// <summary>
        /// 生成。
        /// <para>デフォルトの App.config が使用される。</para>
        /// </summary>
        public AppConfiguration(AppConfigurationInitializeParameters initializeParameters)
            : this(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None), initializeParameters)
        { }

        /// <summary>
        /// 指定した構成ファイルから生成。
        /// </summary>
        /// <param name="configuration">構成ファイル。</param>
        public AppConfiguration(System.Configuration.Configuration configuration, AppConfigurationInitializeParameters initializeParameters)
        {
            Configuration = configuration;
            InitializeParameters = initializeParameters;
        }

        #region proeprty

        /// <summary>
        /// 構成ファイル。
        /// </summary>
        private System.Configuration.Configuration Configuration { get; }

        private AppConfigurationInitializeParameters InitializeParameters { get; }

        private IReadOnlyDictionary<string, string> ReplaceMap { get; set; }

        #endregion

        #region function

        /// <summary>
        /// アプリケーション構成ファイルパスから <see cref="AppConfiguration"/> を生成。
        /// </summary>
        /// <param name="path">アプリケーション構成ファイルパス。</param>
        /// <returns></returns>
        public static AppConfiguration Open(string path, AppConfigurationInitializeParameters initializeParameters)
        {
            var map = new ExeConfigurationFileMap
            {
                ExeConfigFilename = path,
            };
            var conf = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            return new AppConfiguration(conf, initializeParameters);
        }

        /// <summary>
        /// キーから要素を取得。
        /// </summary>
        /// <param name="key">キー。</param>
        /// <param name="result">取得した要素。</param>
        /// <returns>取得出来たか。</returns>
        private bool TryGet(string key, out KeyValueConfigurationElement result)
        {
            result = Configuration.AppSettings.Settings[key];
            return result != null;
        }

        /// <summary>
        /// 指定したキーが存在するか。
        /// </summary>
        /// <param name="key">キー。</param>
        /// <returns>存在するか。</returns>
        public bool Contains(string key)
        {
            return TryGet(key, out _);
        }

        /// <summary>
        /// キーから要素を取得。
        /// </summary>
        /// <param name="key">キー。</param>
        /// <returns>取得した要素。</returns>
        /// <exception cref="KeyNotFoundException">取得に失敗。</exception>
        private KeyValueConfigurationElement Get(string key)
        {
            if (TryGet(key, out var result))
            {
                return result;
            }

            throw new KeyNotFoundException(key);
        }

        /// <summary>
        /// 設定値を指定した型に変換。
        /// </summary>
        /// <typeparam name="TResult">変換先の型。</typeparam>
        /// <param name="value">設定値。</param>
        /// <returns>変換値。</returns>
        /// <exception cref="AppConfigurationException">変換に失敗。</exception>
        private TResult ConvertValue<TResult>(string value)
        {
            try
            {
                var type = typeof(TResult);

                if (type.IsEnum)
                {
                    return (TResult)Enum.Parse(type, value, true);
                }

                if (type == typeof(Uri))
                {
                    return (TResult)(object)new Uri(value);
                }
                if (type == typeof(TimeSpan))
                {
                    if (!TimeSpan.TryParse(value, out var result))
                    {
                        result = System.Xml.XmlConvert.ToTimeSpan(value);
                    }
                    return (TResult)(object)result;
                }
                if (type == typeof(DateTimeOffset))
                {
                    return (TResult)(object)DateTimeOffset.Parse(value);
                }
                if (type == typeof(Guid))
                {
                    return (TResult)(object)Guid.Parse(value);
                }

                return (TResult)Convert.ChangeType(value, type);
            }
            catch (Exception ex)
            {
                throw new AppConfigurationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// キーから指定型の値を取得。
        /// </summary>
        /// <typeparam name="TResult">変換先の型。</typeparam>
        /// <param name="key">キー。</param>
        /// <returns>変換した値。</returns>
        /// <exception cref="AppConfigurationException">変換に失敗。</exception>
        public TResult GetValue<TResult>(string key)
        {
            var rawValue = Get(key).Value;

            return ConvertValue<TResult>(rawValue);
        }

        /// <summary>
        /// キーから指定型の配列を取得。
        /// </summary>
        /// <typeparam name="TResult">変換先の型。</typeparam>
        /// <param name="key">キー。</param>
        /// <param name="separator">設定値の要素区切り文字。</param>
        /// <returns>変換した値の配列。</returns>
        /// <exception cref="AppConfigurationException">変換に失敗。</exception>
        public TResult[] GetValues<TResult>(string key, char separator = ',')
        {
            var rawValue = Get(key).Value;


            // StringSplitOptions.RemoveEmptyEntries は設定ミスに気付かなさそう
            var rawValues = rawValue.Split(new[] { separator }, StringSplitOptions.None);

            if (rawValues.Length == 0)
            {
                return Array.Empty<TResult>();
            }

            return rawValues
                .Select(a => a.Trim())
                .Select(a => ConvertValue<TResult>(a))
                .ToArray()
            ;
        }

        private static Dictionary<string, string> CreateReplaceParameters(AppConfigurationInitializeParameters parameters)
        {
            var map = new Dictionary<string, string>()
            {
                ["DIR:APP"] = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                ["STARTUP-TIMESTAMP:LOCAL:FILE"] = parameters.UtcTimestamp.ToLocalTime().ToString("yyyy-MM-dd_HHmmss"),
                ["APP:NAME"] = parameters.AssemblyName.Name,
                ["APP:VERSION"] = parameters.AssemblyName.Version.ToString(),
                ["APP:REVISION:SHORT"] = parameters.Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion.Substring(0, 8),
                ["APP:REVISION:LONG"] = parameters.Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
            };

            return map;
        }

        public string Replace(string source)
        {
            if (ReplaceMap == null)
            {
                ReplaceMap = CreateReplaceParameters(InitializeParameters);
            }

            return Strings.ReplaceFromDictionary(source, ReplaceMap);
        }

        #endregion
    }
}
