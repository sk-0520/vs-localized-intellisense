using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsLocalizedIntellisense.Models.Mvvm.Message;

namespace VsLocalizedIntellisense.Test.Models.Mvvm.Message
{
    [TestClass]
    public class MessengerTest
    {
        #region function

        private class ActionMessage: IMessage
        {
            public ActionMessage(string messageId = "")
            {
                MessageId = messageId;
            }

            public string MessageId { get; }
        }

        [TestMethod]
        public void Scenario_Action_Test()
        {
            var messenger = new Messenger();
            var callCount = 0;
            var messageItem = messenger.Register<ActionMessage>(m => callCount += 1);

            Assert.AreEqual(0, callCount);
            messenger.Send(new ActionMessage());
            Assert.AreEqual(1, callCount);

            messenger.Send(new ActionMessage("no-hit"));
            Assert.AreEqual(1, callCount);

            messenger.Send(new ActionMessage());
            Assert.AreEqual(2, callCount);

            messenger.Unregister(messageItem);

            messenger.Send(new ActionMessage());
            Assert.AreEqual(2, callCount);
        }

        [TestMethod]
        public async Task Scenario_Function_Test()
        {
            var messenger = new Messenger();
            var callCount = 0;
            var messageItem = messenger.Register<ActionMessage>(m => Task.FromResult(callCount += 1));

            Assert.AreEqual(0, callCount);
            messenger.Send(new ActionMessage());
            Assert.AreEqual(0, callCount);

            await messenger.SendAsync(new ActionMessage());
            Assert.AreEqual(1, callCount);

            await messenger.SendAsync(new ActionMessage("no-hit"));
            Assert.AreEqual(1, callCount);

            await messenger.SendAsync(new ActionMessage());
            Assert.AreEqual(2, callCount);

            messenger.Unregister(messageItem);

            await messenger.SendAsync(new ActionMessage());
            Assert.AreEqual(2, callCount);

        }

        #endregion
    }
}
