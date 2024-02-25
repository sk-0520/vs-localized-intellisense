using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models;

namespace VsLocalizedIntellisense.Test.Models
{
    [TestClass]
    public class LinqExtensionsTest
    {
        #region function

        [TestMethod]
        public void IndexOf_IReadOnlyList_Test()
        {
            IReadOnlyList<int> items = new[] { 10, 20, 30 }.ToList();
            var actual = items.IndexOf(20);
            Assert.AreEqual(1, actual);
            Assert.AreEqual(-1, items.IndexOf(40));
        }

        private class Test_IndexOf_IReadOnlyCollection : IReadOnlyCollection<int>
        {
            private int[] _items = new[] { 10, 20, 30 };

            public int Count => this._items.Length;

            public IEnumerator<int> GetEnumerator()
            {
                return this._items.AsEnumerable().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [TestMethod]
        public void IndexOf_IReadOnlyCollection_Test()
        {
            IReadOnlyCollection<int> items = new Test_IndexOf_IReadOnlyCollection();
            var actual = items.IndexOf(20);
            Assert.AreEqual(1, actual);

            Assert.AreEqual(-1, items.IndexOf(40));
        }

        #endregion
    }
}
