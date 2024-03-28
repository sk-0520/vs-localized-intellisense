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
    public class IntellisenseXmlExtensionsTest
    {
        #region function

        [TestMethod]
        [DataRow(0, "<doc><members></members></doc>")]
        [DataRow(1, "<doc><members><member></member></members></doc>")]
        [DataRow(2, "<doc><members><member/><member/></members></doc>")]
        [DataRow(1, "<doc><members><member><member></member></member></members></doc>")]
        [DataRow(2, "<doc><members><member/><person/><member/></members></doc>")]
        public void GetMembersTest(int expected, string xml)
        {
            var test = XDocument.Parse(xml);
            var actual = test.GetMembers();
            Assert.AreEqual(expected, actual.Count());
        }

        [TestMethod]
        public void GetMembers_root_throw_Test()
        {
            var test = XDocument.Parse("<root/>");
            test.RemoveNodes();
            Assert.ThrowsException<IntellisenseXmlRootNotFoundException>(() => test.GetMembers());
        }

        [TestMethod]
        public void GetMembers_members_throw_Test()
        {
            var test = XDocument.Parse("<root/>");
            Assert.ThrowsException<IntellisenseXmlElementNotFoundException>(() => test.GetMembers());
        }

        [TestMethod]
        public void GetMemberNameTest()
        {
            var test = XDocument.Parse("<root><element name='value' /></root>");
            var element = test.Root!.Element("element")!;
            var actual = element.GetMemberName();
            Assert.AreEqual("value", actual);
        }

        [TestMethod]
        public void GetMemberName_throw_Test()
        {
            var test = XDocument.Parse("<root><element NAME='value' /></root>");
            var element = test.Root!.Element("element")!;
            Assert.ThrowsException<IntellisenseXmlAttributeNotFoundException>(() => element.GetMemberName());
        }

        [TestMethod]
        public void GetMemberMapTest()
        {
            var test = XDocument.Parse("<root><member name='A' /><member name='B' /></root>");
            var elements = test.Root!.Elements("member")!;
            var actual = elements.GetMemberMap();
            Assert.AreEqual("A", actual["A"].Attribute("name")!.Value);
            Assert.AreEqual("B", actual["B"].Attribute("name")!.Value);
        }

        #endregion
    }
}
