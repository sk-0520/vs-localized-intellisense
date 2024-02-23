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
    public class ActionDisposerTest
    {
        [TestMethod]
        public void UsingTest()
        {
            using (var disposer = new ActionDisposer(disposing =>
            {
                Assert.IsTrue(disposing);
            }))
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void FinalizeTest()
        {
            var disposer = new ActionDisposer(disposing =>
            {
                Assert.IsFalse(disposing);
            });
        }
    }

}
