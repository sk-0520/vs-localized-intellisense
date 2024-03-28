using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace VsLocalizedIntellisense.Xml.Models
{
    public class IntellisenseXmlEditor
    {
        #region property

        public string RawAttributeName { get; init; } = "raw";

        #endregion

        #region function

        //public bool UpdateAttribute(XAttribute rawAttribute, XNamespace intellisenseNamespace, XElement intellisenseElement)
        //{
        //    return false;
        //}

        /// <summary>
        /// 対象の要素から多分おんなじの要素を取得する。
        /// <para>順序に依存していいものかわからなかったので無視要素を渡す形に落ち着いた。</para>
        /// </summary>
        /// <param name="rawChildElement">ローカライズ元要素。</param>
        /// <param name="intellisenseParentElement">ローカライズ対象の親要素(<paramref name="rawChildElement"/>と同じ要素を含んでいるはずの親要素)。</param>
        /// <param name="ignoreIntellisenseElements">無視するローカライズ対象要素一覧。</param>
        /// <returns>多分同じのローカライズ対象の親要素。見つからなかった場合は <see langword="null"/>。</returns>
        private XElement? GetProbablySameElement(XElement rawChildElement, XElement intellisenseParentElement, ISet<XElement> ignoreIntellisenseElements)
        {
            var intellisenseChildElements = intellisenseParentElement.Elements(rawChildElement.Name);
            foreach(var intellisenseChildElement in intellisenseChildElements) {
                if(ignoreIntellisenseElements.Contains(intellisenseChildElement)) {
                    continue;
                }

                bool isMatched = true;
                foreach(var rawAttribute in rawChildElement.Attributes()) {
                    var intellisenseAttribute = intellisenseChildElement.Attribute(rawAttribute.Name);
                    if(intellisenseAttribute is null) {
                        isMatched = false;
                        break;
                    }
                    if(rawAttribute.Value != intellisenseAttribute.Value) {
                        isMatched = false;
                    }
                }
                if(isMatched) {
                    return intellisenseChildElement;
                }
            }

            return null;
        }

        public bool UpdateElement(XElement rawElement, XNamespace intellisenseNamespace, XElement intellisenseElement)
        {
            var isChanged = false;

            // 恐らく属性はローカライズ対象ではない(パッと見判断)
            //foreach(var attr in rawElement.Attributes()) {
            //    if(UpdateAttribute(attr, intellisenseNamespace, intellisenseElement)) {
            //        isChanged = true;
            //    }
            //}

            var rawAttributeName = intellisenseNamespace + RawAttributeName;

            var ignoreIntellisenseElements = new HashSet<XElement>();

            // 直下だけでいいはず
            foreach(var rawChildElement in rawElement.Elements()) {
                // 同名要素を取得し、元の文を属性として埋め込む
                // 要素名と属性(インテリセンス用名前空間以外)が同じものが対象とする
                // あくまで存在すれば実施し、存在しないのであれば diff 側の仕事になるのでこちらでは何もしない
                var intellisenseChildElement = GetProbablySameElement(rawChildElement, intellisenseElement, ignoreIntellisenseElements);
                if(intellisenseChildElement is null) {
                    continue;
                }
                ignoreIntellisenseElements.Add(intellisenseChildElement);

                var intellisenseCurrentRawAttribute = intellisenseChildElement.Attribute(rawAttributeName);
                if(intellisenseCurrentRawAttribute is not null) {
                    // 既に設定済みの値が異なる場合に上書きしてしまうと原文が変わったことを diff で検知できないので何もしない
                    continue;
                }

                // 要素内のテキストを生で取得
                using var reader = rawChildElement.CreateReader();
                reader.MoveToContent();
                var rawContent = reader.ReadInnerXml();

                intellisenseChildElement.SetAttributeValue(rawAttributeName, rawContent);
                isChanged = true;
            }

            return isChanged;
        }

        public bool UpdateElements(IReadOnlyDictionary<string, XElement> rawMemberMap, XNamespace intellisenseNamespace, IReadOnlyDictionary<string, XElement> intellisenseMemberMap)
        {
            var isChanged = false;

            foreach(var (rawName, rawElement) in rawMemberMap) {
                if(intellisenseMemberMap.TryGetValue(rawName, out var intellisenseElement)) {
                    if(UpdateElement(rawElement, intellisenseNamespace, intellisenseElement)) {
                        isChanged = true;
                    }
                }
            }

            return isChanged;
        }

        #endregion
    }
}
