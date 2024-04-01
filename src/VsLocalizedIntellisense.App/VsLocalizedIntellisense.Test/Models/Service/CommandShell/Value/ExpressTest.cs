using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Values
{
    public class ExpressTest
    {
        #region function

        [Fact]
        public void ImplicitOperatorTest()
        {
            Express test = "abc";
            Assert.Equal(1, test.Values.Count);
            Assert.IsType<Text>(test.Values[0]);
            Assert.Equal("abc", test.Expression);
        }

        [Fact]
        public void OperatorTest()
        {
            var text = new Text("abc");
            var variable = new Variable("xyz");
            var test1 = text + variable;
            var test2 = variable + text;
            var test3 = text + variable + text;
            Assert.Equal("abc%xyz%", test1.Expression);
            Assert.Equal("%xyz%abc", test2.Expression);
            Assert.Equal("abc%xyz%abc", test3.Expression);

            var test4 = test1 + test2 + test3;
            Assert.Equal("abc%xyz%%xyz%abcabc%xyz%abc", test4.Expression);
        }

        #endregion
    }
}
