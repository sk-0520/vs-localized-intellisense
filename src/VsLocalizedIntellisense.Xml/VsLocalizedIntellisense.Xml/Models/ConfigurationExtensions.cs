using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

namespace VsLocalizedIntellisense.Xml.Models
{
    public static class ConfigurationExtensions
    {
        #region function

        public static T GetRequiredValue<T>(this IConfiguration configuration, string key)
        {
            var result =  configuration.GetValue<T>(key);
            if(result is null) {
                throw new InvalidOperationException(key);
            }

            return result;
        }

        public static string GetRequiredString(this IConfiguration configuration, string key)
        {
            var result = GetRequiredValue<string>(configuration, key);
            if(string.IsNullOrWhiteSpace(result)) {
                throw new InvalidOperationException(key);
            }

            return result;
        }

        public static T GetSectionValue<T>(this IConfigurationSection configurationSection, string key)
        {
            return configurationSection.GetRequiredSection(key).Get<T>() ?? throw new InvalidOperationException(key);
        }

        public static T[] GetSectionValues<T>(this IConfigurationSection configurationSection, string key)
        {
            return configurationSection.GetRequiredSection(key).Get<T[]>() ?? throw new InvalidOperationException(key);
        }

        #endregion
    }
}
