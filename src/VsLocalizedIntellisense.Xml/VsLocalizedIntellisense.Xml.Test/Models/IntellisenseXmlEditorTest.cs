using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VsLocalizedIntellisense.Xml.Models;

namespace VsLocalizedIntellisense.Xml.Test.Models
{
    [TestClass]
    public class IntellisenseXmlEditorTest
    {
        #region function


        [TestMethod]
        public void UpdateElement_normal_Test()
        {
            var rawXml = XDocument.Parse("<root><element><child>abc</child></element></root>");
            var intellisenseXml = XDocument.Parse("<root><element><child/></element></root>");
            var intellisenseNamespace = (XNamespace)"https://example.com/namespace";

            var rawElement = rawXml.Root!.Element("element")!;
            var intellisenseElement = intellisenseXml.Root!.Element("element")!;

            var test = new IntellisenseXmlEditor();
            var actual = test.UpdateElement(rawElement, intellisenseNamespace, intellisenseElement);
            Assert.IsTrue(actual);

            var attribute = intellisenseElement.Element("child")!.Attribute(intellisenseNamespace + test.RawAttributeName);
            Assert.AreEqual("abc", attribute!.Value);
        }

        [TestMethod]
        public void UpdateElement_content_Test()
        {
            var rawXml = XDocument.Parse("<root><element><child>abc<string>STRING</string> <number  value='123' /></child></element></root>");
            var intellisenseXml = XDocument.Parse("<root><element><child/></element></root>");
            var intellisenseNamespace = (XNamespace)"https://example.com/namespace";

            var rawElement = rawXml.Root!.Element("element")!;
            var intellisenseElement = intellisenseXml.Root!.Element("element")!;

            var test = new IntellisenseXmlEditor();
            var actual = test.UpdateElement(rawElement, intellisenseNamespace, intellisenseElement);
            Assert.IsTrue(actual);

            // スペースが詰められたり ' が " になっているのはもういいでしょ(XMLパーサ側のあれこれまではどうでもいい)
            var attribute = intellisenseElement.Element("child")!.Attribute(intellisenseNamespace + test.RawAttributeName);
            Assert.AreEqual("abc<string>STRING</string><number value=\"123\" />", attribute!.Value);
        }

        [TestMethod]
        public void UpdateElement_not_found_Test()
        {
            var rawXml = XDocument.Parse("<root><element><child>abc</child></element></root>");
            var intellisenseXml = XDocument.Parse("<root><element><other/></element></root>");
            var intellisenseNamespace = (XNamespace)"https://example.com/namespace";

            var rawElement = rawXml.Root!.Element("element")!;
            var intellisenseElement = intellisenseXml.Root!.Element("element")!;

            var test = new IntellisenseXmlEditor();
            var actual = test.UpdateElement(rawElement, intellisenseNamespace, intellisenseElement);
            Assert.IsFalse(actual);

            var attribute = intellisenseElement.Element("other")!.Attribute(intellisenseNamespace + test.RawAttributeName);
            Assert.IsNull(attribute);
        }

        [TestMethod]
        public void UpdateElement_multi_Test()
        {
            var rawXml = XDocument.Parse("<root><element><child>abc</child><child>def</child></element></root>");
            var intellisenseXml = XDocument.Parse("<root><element><child/><child/></element></root>");
            var intellisenseNamespace = (XNamespace)"https://example.com/namespace";

            var rawElement = rawXml.Root!.Element("element")!;
            var intellisenseElement = intellisenseXml.Root!.Element("element")!;

            var test = new IntellisenseXmlEditor();
            var actual = test.UpdateElement(rawElement, intellisenseNamespace, intellisenseElement);
            Assert.IsTrue(actual);

            var elements = intellisenseElement.Elements("child").ToArray();
            Assert.AreEqual("abc", elements[0]!.Attribute(intellisenseNamespace + test.RawAttributeName)!.Value);
            Assert.AreEqual("def", elements[1]!.Attribute(intellisenseNamespace + test.RawAttributeName)!.Value);
        }

        [TestMethod]
        public void UpdateElement_no_update_Test()
        {
            var rawXml = XDocument.Parse("<root><element><child>abc</child></element></root>");
            var intellisenseXml = XDocument.Parse("<root xmlns:test='https://example.com/namespace'><element><child test:raw='ABC'/></element></root>");
            var intellisenseNamespace = (XNamespace)"https://example.com/namespace";

            var rawElement = rawXml.Root!.Element("element")!;
            var intellisenseElement = intellisenseXml.Root!.Element("element")!;

            var test = new IntellisenseXmlEditor();
            var actual = test.UpdateElement(rawElement, intellisenseNamespace, intellisenseElement);
            Assert.IsFalse(actual);

            var attribute = intellisenseElement.Element("child")!.Attribute(intellisenseNamespace + test.RawAttributeName);
            Assert.AreEqual("ABC", attribute!.Value);
        }

        #endregion
    }
}
