using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VsLocalizedIntellisense.Xml.Models;
using Xunit;

namespace VsLocalizedIntellisense.Xml.Test.Models
{
    public class IntellisenseXmlExtensionsTest
    {
        #region function

        [Theory]
        [InlineData(0, "<doc><members></members></doc>")]
        [InlineData(1, "<doc><members><member></member></members></doc>")]
        [InlineData(2, "<doc><members><member/><member/></members></doc>")]
        [InlineData(1, "<doc><members><member><member></member></member></members></doc>")]
        [InlineData(2, "<doc><members><member/><person/><member/></members></doc>")]
        public void GetMembersTest(int expected, string xml)
        {
            var test = XDocument.Parse(xml);
            var actual = test.GetMembers();
            Assert.Equal(expected, actual.Count());
        }

        [Fact]
        public void GetMembers_root_throw_Test()
        {
            var test = XDocument.Parse("<root/>");
            test.RemoveNodes();
            Assert.Throws<IntellisenseXmlRootNotFoundException>(() => test.GetMembers());
        }

        [Fact]
        public void GetMembers_members_throw_Test()
        {
            var test = XDocument.Parse("<root/>");
            Assert.Throws<IntellisenseXmlElementNotFoundException>(() => test.GetMembers());
        }

        [Fact]
        public void GetMemberNameTest()
        {
            var test = XDocument.Parse("<root><element name='value' /></root>");
            var element = test.Root!.Element("element")!;
            var actual = element.GetMemberName();
            Assert.Equal("value", actual);
        }

        [Fact]
        public void GetMemberName_throw_Test()
        {
            var test = XDocument.Parse("<root><element NAME='value' /></root>");
            var element = test.Root!.Element("element")!;
            Assert.Throws<IntellisenseXmlAttributeNotFoundException>(() => element.GetMemberName());
        }

        [Fact]
        public void GetMemberMapTest()
        {
            var test = XDocument.Parse("<root><member name='A' /><member name='B' /></root>");
            var elements = test.Root!.Elements("member")!;
            var actual = elements.GetMemberMap();
            Assert.Equal("A", actual["A"].Attribute("name")!.Value);
            Assert.Equal("B", actual["B"].Attribute("name")!.Value);
        }

        #endregion
    }
}
