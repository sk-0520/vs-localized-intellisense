using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Logger;

namespace VsLocalizedIntellisense.Test.Models.Logger
{
    [TestClass]
    public class NullLoggerFactoryTest
    {
        [TestMethod]
        public void ScenarioTest()
        {
            using var test = new NullLoggerFactory();
        }
    }

    [TestClass]
    public class NullLoggerTest
    {
        #region function

        [TestMethod]
        public void Constructor_str_Test()
        {
            var test = new NullLogger("abc");
            Assert.AreEqual("abc", test.CategoryName);
        }

        [TestMethod]
        public void Constructor_none_Test()
        {
            var test = new NullLogger();
            Assert.AreEqual(string.Empty, test.CategoryName);
        }

        #endregion
    }

}
