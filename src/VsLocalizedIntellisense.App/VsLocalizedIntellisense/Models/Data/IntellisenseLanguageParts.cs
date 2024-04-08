using System.Collections.ObjectModel;
using System;

namespace VsLocalizedIntellisense.Models.Data
{
    public class IntellisenseLanguageParts
    {
        public IntellisenseLanguageParts(string intellisenseVersion, string libraryName, string language)
        {
            static string ThrowIfIllegal(string value, string parameterName)
            {
                if(value == null) {
                    throw new ArgumentNullException(parameterName);
                }
                if(string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("null or whitespace", parameterName);
                }

                return value;
            }

            IntellisenseVersion = ThrowIfIllegal(intellisenseVersion, nameof(intellisenseVersion));
            LibraryName = ThrowIfIllegal(libraryName, nameof(libraryName));
            Language = ThrowIfIllegal(language, nameof(language));
        }

        #region property

        public string IntellisenseVersion { get; set; }
        public string LibraryName { get; set; }
        public string Language { get; set; }

        #endregion

        #region object

        public override string ToString()
        {
            return $"{IntellisenseVersion}/{LibraryName}/{Language}";
        }
        #endregion
    }
}
