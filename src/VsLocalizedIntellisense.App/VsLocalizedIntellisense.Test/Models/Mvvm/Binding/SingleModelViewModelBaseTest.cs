using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Logger;
using VsLocalizedIntellisense.Models.Mvvm.Binding;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Binding
{
    [TestClass]
    public class SingleModelViewModelBaseTest
    {
        #region function

        private class TestModel: BindModelBase
        {
            public int PropertyA { get; set; }
            public int PropertyANext { get; set; }
        }

        private class TestSingleModelViewModel: SingleModelViewModelBase<TestModel>
        {
            public TestSingleModelViewModel(TestModel model, ILoggerFactory loggerFactory)
                : base(model, loggerFactory)
            { }

            public int PropertyA
            {
                get => Model.PropertyA;
                set => SetModel(value);
            }

            public int PropertyB
            {
                get => Model.PropertyANext;
                set => SetModel(value, nameof(Model.PropertyANext));
            }
        }

        [TestMethod]
        public void SetModel_a_Test()
        {
            var model = new TestModel();
            var vm = new TestSingleModelViewModel(model, NullLoggerFactory.Instance);
            bool called = false;
            vm.PropertyChanged += (s, e) => {
                Assert.AreEqual(nameof(vm.PropertyA), e.PropertyName);
                called = true;
            };
            Assert.IsFalse(called);
            vm.PropertyA = 123;
            Assert.IsTrue(called);
            Assert.AreEqual(123, vm.PropertyA);
            Assert.AreEqual(model.PropertyA, vm.PropertyA);
        }

        [TestMethod]
        public void SetModel_b_Test()
        {
            var model = new TestModel();
            var vm = new TestSingleModelViewModel(model, NullLoggerFactory.Instance);
            bool called = false;
            vm.PropertyChanged += (s, e) => {
                Assert.AreEqual(nameof(vm.PropertyB), e.PropertyName);
                called = true;
            };
            Assert.IsFalse(called);
            vm.PropertyB = 123;
            Assert.IsTrue(called);
            Assert.AreEqual(123, vm.PropertyB);
            Assert.AreEqual(model.PropertyANext, vm.PropertyB);
        }

        #endregion
    }

}
