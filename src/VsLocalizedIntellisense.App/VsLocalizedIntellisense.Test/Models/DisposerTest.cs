using System;
using Xunit;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    public class DisposerTest
    {
        private class TestDisposer: DisposerBase
        {
            public bool Disposing { get; private set; }

            protected override void Dispose(bool disposing)
            {
                Disposing = disposing;
                base.Dispose(disposing);
            }

            public void Procedure()
            {
                ThrowIfDisposed();
            }
        }

        [Fact]
        public void Test()
        {
            var disposer = new TestDisposer();

            disposer.Dispose();

            Assert.True(disposer.IsDisposed);
            Assert.True(disposer.Disposing);
        }

        [Fact]
        public void Dispose2Test()
        {
            var disposer = new TestDisposer();

            disposer.Dispose();
            disposer.Dispose();

            Assert.True(disposer.IsDisposed);
            Assert.True(disposer.Disposing);
        }

        [Fact]
        public void ThrowIfDisposedTest()
        {
            var disposer = new TestDisposer();

            disposer.Procedure();
            disposer.Dispose();
            Assert.Throws<ObjectDisposedException>(() => disposer.Procedure());
        }
    }
}
