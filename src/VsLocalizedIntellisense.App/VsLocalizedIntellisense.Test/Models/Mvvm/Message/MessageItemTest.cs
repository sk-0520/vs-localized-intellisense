using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Mvvm.Message;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Message
{
    [TestClass]
    public class MessageItemTest
    {
        [TestMethod]
        public void Constructor_messageId_throw_Test()
        {
            var actual = Assert.ThrowsException<ArgumentNullException>(() => new MessageItem(null, GetType(), this, GetType().GetMethod(nameof(ToString))));
            Assert.AreEqual("messageId", actual.ParamName);
        }

        [TestMethod]
        public void Constructor_messageType_throw_Test()
        {
            var actual = Assert.ThrowsException<ArgumentNullException>(() => new MessageItem("", null, this, GetType().GetMethod(nameof(ToString))));
            Assert.AreEqual("messageType", actual.ParamName);
        }

        [TestMethod]
        public void Constructor_callbackMethodInfo_throw_Test()
        {
            var actual = Assert.ThrowsException<ArgumentNullException>(() => new MessageItem("", GetType(), this, null));
            Assert.AreEqual("callbackMethodInfo", actual.ParamName);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            var actual = new MessageItem("", GetType(), this, GetType().GetMethod(nameof(ToString)));
            Assert.AreEqual("", actual.MessageId);
            Assert.AreEqual(GetType(), actual.MessageType);
            Assert.AreEqual(this, actual.CallbackInstance);
            Assert.AreEqual(GetType().GetMethod(nameof(ToString)), actual.CallbackMethodInfo);
        }

        [TestMethod]
        public void DisposeTest()
        {
            var actual = new MessageItem("", GetType(), this, GetType().GetMethod(nameof(ToString)));
            actual.Dispose();
            Assert.AreEqual("", actual.MessageId);
            Assert.AreEqual(GetType(), actual.MessageType);
            Assert.IsNull(actual.CallbackInstance);
            Assert.AreEqual(GetType().GetMethod(nameof(ToString)), actual.CallbackMethodInfo);
            actual.Dispose();
        }
    }
}
