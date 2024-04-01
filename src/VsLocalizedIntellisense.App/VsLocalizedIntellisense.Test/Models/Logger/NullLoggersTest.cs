using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    public class NullLoggerFactoryTest
    {
        [Fact]
        public void ScenarioTest()
        {
            using var test = new NullLoggerFactory();
        }
    }

    public class NullLoggerTest
    {
        #region function

        [Fact]
        public void Constructor_str_Test()
        {
            var test = new NullLogger("abc");
            Assert.Equal("abc", test.CategoryName);
        }

        [Fact]
        public void Constructor_none_Test()
        {
            var test = new NullLogger();
            Assert.Equal(string.Empty, test.CategoryName);
        }

        #endregion
    }
}
