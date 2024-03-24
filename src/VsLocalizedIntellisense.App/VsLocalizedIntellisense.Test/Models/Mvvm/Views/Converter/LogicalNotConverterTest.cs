using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Mvvm.Views.Converter;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Views.Converter
{
    [TestClass]
    public class LogicalNotConverterTest
    {
        #region function

        [TestMethod]
        [DataRow(false, true)]
        [DataRow(true, false)]
        public void ConvertTest(bool expected, bool value)
        {
            var test = new LogicalNotConverter();
            var actual = test.Convert(value, value.GetType(), null, CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(typeof(InvalidCastException), 0)]
        [DataRow(typeof(InvalidCastException), "")]
        [DataRow(typeof(NullReferenceException), null)]
        public void Convert_throw_Test(Type exception, object value)
        {
            var test = new LogicalNotConverter();
            try {
                test.Convert(value, value?.GetType(), null, CultureInfo.InvariantCulture);
                Assert.Fail();
            } catch (Exception ex) {
                Assert.IsInstanceOfType(ex, exception);
            }
        }

        [TestMethod]
        [DataRow(false, true)]
        [DataRow(true, false)]
        public void ConvertBackTest(bool expected, bool value)
        {
            var test = new LogicalNotConverter();
            var actual = test.ConvertBack(value, value.GetType(), null, CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(typeof(InvalidCastException), 0)]
        [DataRow(typeof(InvalidCastException), "")]
        [DataRow(typeof(NullReferenceException), null)]
        public void ConvertBack_throw_Test(Type exception, object value)
        {
            var test = new LogicalNotConverter();
            try {
                test.ConvertBack(value, value?.GetType(), null, CultureInfo.InvariantCulture);
                Assert.Fail();
            } catch(Exception ex) {
                Assert.IsInstanceOfType(ex, exception);
            }
        }

        #endregion
    }
}
