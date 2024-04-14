using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Element;
using VsLocalizedIntellisense.Models.Logger;
using Xunit;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace VsLocalizedIntellisense.Test.Models.Element
{
    public class IntellisenseVersionElementTest
    {
        #region function

        [Theory]
        [InlineData("name", null, null)]
        [InlineData("version", "", null)]
        public void Constructor_throw_Test(string expectedParamName, string name, Version version)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new IntellisenseVersionElement(name, version, NullLoggerFactory.Instance));
            Assert.Equal(expectedParamName, ex.ParamName);
        }

        [Fact]
        public void PropertyTest()
        {
            var test = new IntellisenseVersionElement("name", new Version(1, 2, 3, 4), NullLoggerFactory.Instance);
            Assert.Equal("name", test.Name);
            Assert.Equal(new Version(1,2,3,4), test.Version);
        }


        [Fact]
        public void DirectoryNameTest()
        {
            var test = new IntellisenseVersionElement("name", new Version(1, 2, 3, 4), NullLoggerFactory.Instance);
            Assert.Equal("name1.2.3.4", test.DirectoryName);
        }

        #endregion
    }
}
