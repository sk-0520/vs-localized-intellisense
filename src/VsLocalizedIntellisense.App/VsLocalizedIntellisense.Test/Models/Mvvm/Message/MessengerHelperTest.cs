using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Mvvm.Message;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Message
{
    [TestClass]
    public class MessengerHelperTest
    {
        #region function

        private class TestGetMessengerFromProperty_empty
        { }

        [TestMethod]
        public void GetMessengerFromProperty_empty_Test()
        {
            var dataContext = new TestGetMessengerFromProperty_empty();
            var actual = MessengerHelper.GetMessengerFromProperty(dataContext);
            Assert.IsNull(actual);
        }


        private class TestGetMessengerFromProperty_private
        {
            [Messenger]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:使用されていないプライベート メンバーを削除する", Justification = "<保留中>")]
            private Messenger Messenger { get; } = new Messenger();
        }

        [TestMethod]
        public void GetMessengerFromProperty_private_Test()
        {
            var dataContext = new TestGetMessengerFromProperty_private();
            var actual = MessengerHelper.GetMessengerFromProperty(dataContext);
            Assert.IsNull(actual);
        }

        private class TestGetMessengerFromProperty_type1
        {
            [Messenger]
            public Uri Messenger { get; } = new Uri("file://NUL");
        }

        [TestMethod]
        public void GetMessengerFromProperty_type1_Test()
        {
            var dataContext = new TestGetMessengerFromProperty_type1();
            var actual = MessengerHelper.GetMessengerFromProperty(dataContext);
            Assert.IsNull(actual);
        }

        private class TestGetMessengerFromProperty_type2
        {
            [Messenger]
            public object Messenger { get; } = new object();
        }

        [TestMethod]
        public void GetMessengerFromProperty_type2_Test()
        {
            var dataContext = new TestGetMessengerFromProperty_type2();
            var actual = MessengerHelper.GetMessengerFromProperty(dataContext);
            Assert.IsNull(actual);
        }

        private class TestGetMessengerFromProperty_null
        {
            [Messenger]
            public Messenger Messenger { get; } = null;
        }

        [TestMethod]
        public void GetMessengerFromProperty_null_Test()
        {
            var dataContext = new TestGetMessengerFromProperty_null();
            var actual = MessengerHelper.GetMessengerFromProperty(dataContext);
            Assert.IsNull(actual);
        }

        private class TestGetMessengerFromProperty_success
        {
            [Messenger]
            public Messenger Messenger { get; } = new Messenger();
        }

        [TestMethod]
        public void TestGetMessengerFromProperty_success_scoped_Test()
        {
            var dataContext = new TestGetMessengerFromProperty_success();
            var actual = MessengerHelper.GetMessengerFromProperty(dataContext);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ScopedMessenger));
        }

        [TestMethod]
        public void TestGetMessengerFromProperty_success_raw_Test()
        {
            var dataContext = new TestGetMessengerFromProperty_success();
            var actual = MessengerHelper.GetMessengerFromProperty(dataContext, true);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(Messenger));
        }

        #endregion
    }
}
