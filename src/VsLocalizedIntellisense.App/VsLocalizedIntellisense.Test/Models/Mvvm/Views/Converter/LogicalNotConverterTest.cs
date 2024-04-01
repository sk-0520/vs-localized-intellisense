using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using VsLocalizedIntellisense.Models.Mvvm.Views.Converter;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Views.Converter
{
    public class LogicalNotConverterTest
    {
        #region function

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void ConvertTest(bool expected, bool value)
        {
            var test = new LogicalNotConverter();
            var actual = test.Convert(value, value.GetType(), null, CultureInfo.InvariantCulture);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(typeof(InvalidCastException), 0)]
        [InlineData(typeof(InvalidCastException), "")]
        [InlineData(typeof(NullReferenceException), null)]
        public void Convert_throw_Test(Type exception, object value)
        {
            var test = new LogicalNotConverter();
            try {
                test.Convert(value, value?.GetType(), null, CultureInfo.InvariantCulture);
                Assert.Fail();
            } catch (Exception ex) {
                Assert.IsType(exception, ex);
            }
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void ConvertBackTest(bool expected, bool value)
        {
            var test = new LogicalNotConverter();
            var actual = test.ConvertBack(value, value.GetType(), null, CultureInfo.InvariantCulture);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(typeof(InvalidCastException), 0)]
        [InlineData(typeof(InvalidCastException), "")]
        [InlineData(typeof(NullReferenceException), null)]
        public void ConvertBack_throw_Test(Type exception, object value)
        {
            var test = new LogicalNotConverter();
            try {
                test.ConvertBack(value, value?.GetType(), null, CultureInfo.InvariantCulture);
                Assert.Fail();
            } catch(Exception ex) {
                Assert.IsType(exception, ex);
            }
        }

        #endregion
    }
}
