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
    public class LanguageElementTest
    {
        #region function

        [Fact]
        public void Constructor_throw_Test() {
            var ex = Assert.Throws<ArgumentNullException>(() => new LanguageElement(null, NullLoggerFactory.Instance));
            Assert.Equal("language", ex.ParamName);
        }

        [Fact]
        public void PropertyTest()
        {
            var test = new LanguageElement("lang", NullLoggerFactory.Instance);
            Assert.Equal("lang", test.Language);
        }

        #endregion
    }
}
