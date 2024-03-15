using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Mvvm;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.ViewModels;
using VsLocalizedIntellisense.Models.Mvvm.Binding;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Binding
{
    [TestClass]
    public class ViewModelBaseTest
    {
        #region function

        private class TestModel
        {
            private int PrivateValue { get; set; }
            public int PublicValue { get; set; }

            public int GetPrivateValue() => PrivateValue;
        }

        private class TestNestedModel
        {
            public int Value { get; set; }
        }

        private class TestViewModel : ViewModelBase
        {
            public TestViewModel()
                : base(NullLoggerFactory.Instance)
            { }

            private TestModel TestModel { get; } = new TestModel();
            private TestNestedModel TestNestedModel { get; } = new TestNestedModel();

            public int PrivateValue
            {
                get => TestModel.GetPrivateValue();
                set => SetProperty(TestModel, value);
            }

            public int PublicValue
            {
                get => TestModel.PublicValue;
                set => SetProperty(TestModel, value);
            }

            public int AliasValue
            {
                get => TestNestedModel.Value;
                set => SetProperty(TestNestedModel, value, nameof(TestNestedModel.Value));
            }
        }

        [TestMethod]
        public void SetProperty_public_Test()
        {
            var tvm = new TestViewModel();
            bool called = false;
            tvm.PropertyChanged += (s, e) =>
            {
                Assert.AreEqual(nameof(TestViewModel.PublicValue), e.PropertyName);
                called = true;
            };
            Assert.IsFalse(called);
            tvm.PublicValue = 123;
            Assert.IsTrue(called);
            Assert.AreEqual(123, tvm.PublicValue);
        }

        [TestMethod]
        public void SetProperty_private_Test()
        {
            var tvm = new TestViewModel();
            bool called = false;
            tvm.PropertyChanged += (s, e) =>
            {
                Assert.AreEqual(nameof(TestViewModel.PrivateValue), e.PropertyName);
                called = true;
            };
            Assert.IsFalse(called);
            tvm.PrivateValue = 123;
            Assert.IsTrue(called);
            Assert.AreEqual(123, tvm.PrivateValue);
        }

        [TestMethod]
        public void SetProperty_alias_Test()
        {
            var tvm = new TestViewModel();
            bool called = false;
            tvm.PropertyChanged += (s, e) =>
            {
                Assert.AreEqual(nameof(TestViewModel.AliasValue), e.PropertyName);
                called = true;
            };
            Assert.IsFalse(called);
            tvm.AliasValue = 123;
            Assert.IsTrue(called);
            Assert.AreEqual(123, tvm.AliasValue);
        }

        #endregion
    }
}
