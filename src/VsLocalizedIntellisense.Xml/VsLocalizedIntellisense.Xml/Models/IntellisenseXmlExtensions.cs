using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VsLocalizedIntellisense.Xml.Models
{
    public static class IntellisenseXmlExtensions
    {
        #region function

        public static IEnumerable<XElement> GetMembers(this XDocument xml)
        {
            var root = xml.Root ?? throw new IntellisenseXmlRootNotFoundException("root");
            var membersElement = root.Element("members") ?? throw new IntellisenseXmlElementNotFoundException("members");
            return membersElement.Elements("member");
        }

        private static string GetAttributeValue(XElement element, XName name)
        {
            var attribute = element.Attribute(name);
            if(attribute is null) {
                throw new IntellisenseXmlAttributeNotFoundException(name.ToString());
            }

            return attribute.Value;
        }

        public static string GetMemberName(this XElement element)
        {
            return GetAttributeValue(element, "name");
        }

        public static Dictionary<string, XElement> GetMemberMap(this IEnumerable<XElement> elements)
        {
            return elements.ToDictionary(k => k.GetMemberName(), v => v);
        }


        #endregion
    }
}
