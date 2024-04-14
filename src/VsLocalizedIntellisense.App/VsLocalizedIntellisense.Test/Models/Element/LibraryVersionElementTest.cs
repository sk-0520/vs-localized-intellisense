using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using Xunit;

namespace VsLocalizedIntellisense.Test.Models.Element
{
    public class LibraryVersionElementTest
    {
        #region function

        [Fact]
        public void Constructor_throw_Test()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new LibraryVersionElement(null, NullLoggerFactory.Instance));
            Assert.Equal("rawVersion", ex.ParamName);
        }

        [Fact]
        public void PropertyTest()
        {
            var test = new LibraryVersionElement("1.2.3.4", NullLoggerFactory.Instance);
            Assert.Equal(new Version(1,2,3,4), test.Version);
        }

        #endregion
    }
}
