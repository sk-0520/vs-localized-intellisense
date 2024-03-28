using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsLocalizedIntellisense.Xml.Models
{
    [Serializable]
    public class IntellisenseException: Exception
    {
        public IntellisenseException()
        { }
        public IntellisenseException(string message)
            : base(message)
        { }
        public IntellisenseException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    public class IntellisenseXmlException: IntellisenseException
    {
        public IntellisenseXmlException()
        { }
        public IntellisenseXmlException(string message)
            : base(message)
        { }
        public IntellisenseXmlException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    public class IntellisenseXmlRootNotFoundException: IntellisenseXmlException
    {
        public IntellisenseXmlRootNotFoundException()
        { }
        public IntellisenseXmlRootNotFoundException(string message)
            : base(message)
        { }
        public IntellisenseXmlRootNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    public class IntellisenseXmlElementNotFoundException: IntellisenseXmlException
    {
        public IntellisenseXmlElementNotFoundException()
        { }
        public IntellisenseXmlElementNotFoundException(string message)
            : base(message)
        { }
        public IntellisenseXmlElementNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    public class IntellisenseXmlAttributeException: IntellisenseXmlException
    {
        public IntellisenseXmlAttributeException()
        { }
        public IntellisenseXmlAttributeException(string message)
            : base(message)
        { }
        public IntellisenseXmlAttributeException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    public class IntellisenseXmlAttributeNotFoundException: IntellisenseXmlAttributeException
    {
        public IntellisenseXmlAttributeNotFoundException()
        { }
        public IntellisenseXmlAttributeNotFoundException(string message)
            : base(message)
        { }
        public IntellisenseXmlAttributeNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }
    }

}
