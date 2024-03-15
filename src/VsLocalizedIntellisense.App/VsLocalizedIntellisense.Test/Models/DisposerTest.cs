using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    [TestClass]
    public class DisposerTest
    {
        private class TestDisposer : DisposerBase
        {
            public bool Disposing { get; private set; }

            protected override void Dispose(bool disposing)
            {
                Disposing = disposing;
                base.Dispose(disposing);
            }
        }

        [TestMethod]
        public void Test()
        {
            var disposer = new TestDisposer();

            disposer.Dispose();

            Assert.IsTrue(disposer.IsDisposed);
            Assert.IsTrue(disposer.Disposing);
        }

        [TestMethod]
        public void Dispose2Test()
        {
            var disposer = new TestDisposer();

            disposer.Dispose();
            disposer.Dispose();

            Assert.IsTrue(disposer.IsDisposed);
            Assert.IsTrue(disposer.Disposing);
        }
    }
}
