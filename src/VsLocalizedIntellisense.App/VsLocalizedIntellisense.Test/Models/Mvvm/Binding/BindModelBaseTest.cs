using Xunit;
using VsLocalizedIntellisense.Models.Mvvm.Binding;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Binding
{
    public class BindModelBaseTest
    {
        #region function

        private class TestBindModel: BindModelBase
        {
            private int variable;

            public int GetVariableTest() => this.variable;
            public bool SetVariableTest(int value) => SetVariable(ref this.variable, value, nameof(this.variable));
        }

        [Fact]
        public void SetVariableTest()
        {
            var tbm = new TestBindModel();
            int changeCount = 0;
            var variableValue = 123;
            tbm.PropertyChanged += (s, e) => {
                changeCount += 1;
                Assert.Equal("variable", e.PropertyName);
                Assert.Equal(variableValue, tbm.GetVariableTest());
            };
            Assert.True(tbm.SetVariableTest(variableValue));
            Assert.Equal(variableValue, tbm.GetVariableTest());
            Assert.Equal(1, changeCount);

            Assert.False(tbm.SetVariableTest(variableValue));
            Assert.Equal(variableValue, tbm.GetVariableTest());
            Assert.Equal(1, changeCount);

            variableValue = 456;
            Assert.True(tbm.SetVariableTest(variableValue));
            Assert.Equal(variableValue, tbm.GetVariableTest());
            Assert.Equal(2, changeCount);
        }

        #endregion
    }
}
