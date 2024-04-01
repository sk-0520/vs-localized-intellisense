using Xunit;
using VsLocalizedIntellisense.Models.Service.CommandShell;

namespace VsLocalizedIntellisense.Test.Models.Service.CommandPrompt
{
    public class IndentContextTest
    {
        #region function

        [Fact]
        public void NestTest()
        {
            var root = new IndentContext();
            var child = root.Nest();

            Assert.Equal(root.Space, child.Space);
            Assert.Equal(0, root.Level);
            Assert.Equal(1, child.Level);
        }

        #endregion
    }
}
