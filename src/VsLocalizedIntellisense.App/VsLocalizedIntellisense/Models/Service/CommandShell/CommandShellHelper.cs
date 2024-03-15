using System;
using System.Linq;

namespace VsLocalizedIntellisense.Models.Service.CommandShell
{
    public static class CommandShellHelper
    {
        #region property

        public const string IndentSpace = "\t";

        #endregion

        #region function

        public static string Escape(string raw)
        {
            var needQuotation = raw == string.Empty;
            var needHatEscape = false;

            needQuotation |= raw.Contains(' ');
            needQuotation |= raw.Contains('%');

            // % はむり
            needHatEscape |= raw.Contains('^');
            needHatEscape |= raw.Contains('<');
            needHatEscape |= raw.Contains('>');

            if(needHatEscape) {
                raw = raw
                    .Replace("^", "^^")
                    .Replace("<", "^<")
                    .Replace(">", "^>")
                ;
            }

            if(needQuotation) {
                return '"' + raw + '"';
            }

            return raw;
        }

        public static string ToSafeVariableName(string name)
        {
            if(string.IsNullOrWhiteSpace(name)) {
                throw new ArgumentException(nameof(name));
            }

            var buffer = name.Trim().ToArray();
            for(var i = 0; i < buffer.Length; i++) {
                var c = buffer[i];
                var isSafe = ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || ('0' <= c && c <= '9') || c == '_';
                if(!isSafe) {
                    buffer[i] = '_';
                }
            }

            return new string(buffer);
        }

        #endregion
    }
}
