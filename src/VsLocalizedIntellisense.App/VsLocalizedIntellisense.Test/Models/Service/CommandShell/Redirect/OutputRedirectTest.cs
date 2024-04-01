using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Redirect;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Redirect
{
    public class OutputRedirectTest
    {
        #region function

        [Fact]
        public void Expression_none_Test()
        {
            var test = new OutputRedirect();
            Assert.Equal("", test.Expression);
        }

        [Fact]
        public void Expression_std_Test()
        {
            var test = new OutputRedirect() {
                Target = "OUT",
            };
            Assert.Equal("> OUT", test.Expression);
        }

        [Fact]
        public void Expression_error_StandardOutput_Test()
        {
            var test = new OutputRedirect() {
                Error = new ErrorRedirect() {
                    StandardOutput = true,
                },
            };
            Assert.Equal("2>&1", test.Expression);

            test.Target = "OUT";
            Assert.Equal("> OUT 2>&1", test.Expression);
        }

        [Fact]
        public void Expression_error_redirect_Test()
        {
            var test = new OutputRedirect() {
                Error = new ErrorRedirect() {
                    Target = "ERR"
                },
            };
            Assert.Equal("2> ERR", test.Expression);

            test.Target = "OUT";
            Assert.Equal("> OUT 2> ERR", test.Expression);
        }

        [Fact]
        public void Expression_error_none_Test()
        {
            var test = new OutputRedirect() {
                Error = new ErrorRedirect(),
            };
            Assert.Equal("", test.Expression);
        }

        #endregion
    }
}
