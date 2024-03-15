using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    [TestClass]
    public class NotifyModelBaseTest
    {
        #region function

        internal class TestClass: NotifyPropertyBase
        {
            #region variable

            private int _value;

            #endregion

            #region proeprty

            public int Value
            {
                get => this._value;
                set
                {
                    this._value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }

            #endregion
        }

        [TestMethod]
        public void Test()
        {
            var ts = new TestClass();

            int count = 0;

            ts.PropertyChanged += (s, e) => {
                Assert.AreEqual(nameof(TestClass.Value), e.PropertyName);
                Assert.AreEqual(count, ts.Value);
            };

            ts.Value = ++count;
            ts.Value = ++count;
        }

        #endregion
    }
}
