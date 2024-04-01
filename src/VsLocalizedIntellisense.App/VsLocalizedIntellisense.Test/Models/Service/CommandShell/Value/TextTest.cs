using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell.Value;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandShell.Value
{
    public class TextTest
    {
        #region function

        [Fact]
        public void Test()
        {
            var test = new Text("abc");
            Assert.Equal("abc", test.Data);
            Assert.Equal("abc", test.Expression);
        }

        #endregion
    }
}
