using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    [TestClass]
    public class NotifyPropertyBaseTest
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

            #region function

            public void CallRaisePropertyChanged(int v)
            {
                this._value = v;
                RaisePropertyChanged(nameof(Value));
            }

            #endregion
        }

        [TestMethod]
        public void SubscribeTest()
        {
            var ts = new TestClass();

            int count = 0;

            ts.PropertyChanged += (s, e) => {
                Assert.AreEqual(nameof(TestClass.Value), e.PropertyName);
                Assert.AreEqual(count, ts.Value);
            };

            ts.Value = ++count;
            ts.Value = ++count;

            Assert.AreEqual(count, ts.Value);

            count = 100;
            ts.CallRaisePropertyChanged(count);
            Assert.AreEqual(count, ts.Value);
        }

        [TestMethod]
        public void UnsubscribeTest()
        {
            var ts = new TestClass();

            int count = 0;

            ts.Value = ++count;
            ts.Value = ++count;

            Assert.AreEqual(count, ts.Value);


            count = 100;
            ts.CallRaisePropertyChanged(count);
            Assert.AreEqual(count, ts.Value);
        }

        #endregion
    }
}
