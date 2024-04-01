using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Redirect;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Redirect
{
    public class RedirectBaseTest
    {
        private class TestRedirect: RedirectBase
        { }

        #region function

        [Theory]
        [InlineData("> a", "a", false)]
        [InlineData(">> a", "a", true)]
        public void ExpressionTest(string expected, string target, bool append)
        {
            var test = new TestRedirect() {
                Target = target,
                Append = append,
            };
            var actual = test.Expression;
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
