using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Models
{
    public static class Strings
    {
        #region function

        /// <summary>
        /// 指定範囲の値を指定処理で置き換える。
        /// </summary>
        /// <param name="source">対象。</param>
        /// <param name="head">置き換え開始文字列。</param>
        /// <param name="tail">置き換え終了文字列。</param>
        /// <param name="dg">処理。</param>
        /// <returns>置き換え後文字列。</returns>
        public static string ReplacePlaceholder(string source, string head, string tail, Func<string, string> dg)
        {
            var escHead = Regex.Escape(head);
            var escTail = Regex.Escape(tail);
            var pattern = escHead + "(.+?)" + escTail;
            var replacedText = Regex.Replace(source, pattern, (m) => dg(m.Groups[1].Value));
            return replacedText;
        }

        /// <summary>
        /// 指定範囲の値を指定のコレクションで置き換える。
        /// </summary>
        /// <param name="source">対象。</param>
        /// <param name="head">置き換え開始文字列。</param>
        /// <param name="tail">置き換え終了文字列。</param>
        /// <param name="map">置き換え対象文字列と置き換え後文字列のペアであるコレクション。</param>
        /// <returns>置き換え後文字列。</returns>
        public static string ReplacePlaceholderFromDictionary(string source, string head, string tail, IReadOnlyDictionary<string, string> map)
        {
            return ReplacePlaceholder(source, head, tail, s => map.ContainsKey(s) ? map[s] : head + s + tail);
        }
        /// <summary>
        /// 文字列中の<c>${key}</c>を<see cref="IReadOnlyDictionary{string, string}"/>の対応で置き換える。
        /// </summary>
        /// <param name="source">対象文字列。</param>
        /// <param name="map">マップ。</param>
        /// <returns>置き換え後文字列。</returns>
        public static string ReplaceFromDictionary(string source, IReadOnlyDictionary<string, string> map)
        {
            return ReplacePlaceholderFromDictionary(source, "${", "}", map);
        }

        #endregion
    }
}
