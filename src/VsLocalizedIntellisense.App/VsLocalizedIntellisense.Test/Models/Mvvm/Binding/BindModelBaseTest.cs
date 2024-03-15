using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Mvvm;
using VsLocalizedIntellisense.Models.Mvvm.Binding;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Binding
{
    [TestClass]
    public class BindModelBaseTest
    {
        #region function

        private class TestBindModel : BindModelBase
        {
            private int variable;

            public int GetVariableTest() => this.variable;
            public bool SetVariableTest(int value) => SetVariable(ref this.variable, value, nameof(this.variable));
        }

        [TestMethod]
        public void SetVariableTest()
        {
            var tbm = new TestBindModel();
            int changeCount = 0;
            var variableValue = 123;
            tbm.PropertyChanged += (s, e) =>
            {
                changeCount += 1;
                Assert.AreEqual("variable", e.PropertyName);
                Assert.AreEqual(variableValue, tbm.GetVariableTest());
            };
            Assert.IsTrue(tbm.SetVariableTest(variableValue));
            Assert.AreEqual(variableValue, tbm.GetVariableTest());
            Assert.AreEqual(1, changeCount);

            Assert.IsFalse(tbm.SetVariableTest(variableValue));
            Assert.AreEqual(variableValue, tbm.GetVariableTest());
            Assert.AreEqual(1, changeCount);

            variableValue = 456;
            Assert.IsTrue(tbm.SetVariableTest(variableValue));
            Assert.AreEqual(variableValue, tbm.GetVariableTest());
            Assert.AreEqual(2, changeCount);
        }

        #endregion
    }
}
