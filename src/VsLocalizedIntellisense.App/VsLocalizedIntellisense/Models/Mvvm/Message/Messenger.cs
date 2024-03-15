using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace VsLocalizedIntellisense.Models.Mvvm.Message
{
    public interface IReceivableMessenger
    {
        MessageItem Register<TMessage>(Action<TMessage> action, string messageId = "") where TMessage : IMessage;
        MessageItem Register<TMessage>(Func<TMessage, Task> callback, string messageId = "") where TMessage : IMessage;
        void Unregister(MessageItem messageItem);
    }

    public interface ISendableMessenger
    {
        void Send<TMessage>(TMessage message) where TMessage : IMessage;
        Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : IMessage;
    }

    public interface IMessenger : IReceivableMessenger, ISendableMessenger
    { }

    public class Messenger : DisposerBase, IMessenger
    {
        public Messenger()
        { }

        #region property

        private Dictionary<Type, List<MessageItem>> RegisteredSyncTypes { get; } = new Dictionary<Type, List<MessageItem>>();
        private Dictionary<Type, List<MessageItem>> RegisteredAsyncTypes { get; } = new Dictionary<Type, List<MessageItem>>();

        #endregion

        #region function

        public MessageItem Register<TMessage>(Action<TMessage> action, string messageId = "")
            where TMessage : IMessage
        {
            var messageType = typeof(TMessage);

            if (!RegisteredSyncTypes.TryGetValue(messageType, out var messages))
            {
                messages = new List<MessageItem>();
                RegisteredSyncTypes.Add(messageType, messages);
            }

            var item = new MessageItem(messageId, messageType, action.Target, action.Method);
            messages.Add(item);
            return item;
        }

        public MessageItem Register<TMessage>(Func<TMessage, Task> callback, string messageId = "")
            where TMessage : IMessage
        {
            var messageType = typeof(TMessage);

            if (!RegisteredAsyncTypes.TryGetValue(messageType, out var messages))
            {
                messages = new List<MessageItem>();
                RegisteredAsyncTypes.Add(messageType, messages);
            }

            var item = new MessageItem(messageId, messageType, callback.Target, callback.Method);
            messages.Add(item);
            return item;
        }

        private bool UnregisterCore(MessageItem messageItem, Dictionary<Type, List<MessageItem>> registeredTypes)
        {
            if (registeredTypes.TryGetValue(messageItem.MessageType, out var messages))
            {
                if (messages.Remove(messageItem))
                {
                    messageItem.Dispose();

                    return true;
                }
            }

            return false;
        }

        public void Unregister(MessageItem messageItem)
        {
            ThrowIfDisposed();

            if (!UnregisterCore(messageItem, RegisteredSyncTypes))
            {
                UnregisterCore(messageItem, RegisteredAsyncTypes);
            }
        }

        private MessageItem SearchMessageItem(IMessage needle, IReadOnlyList<MessageItem> haystack)
        {
            return haystack.FirstOrDefault(a => a.MessageId == needle.MessageId);
        }

        public void Send<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            ThrowIfDisposed();

            if (!RegisteredSyncTypes.TryGetValue(typeof(TMessage), out var messages))
            {
                return;
            }

            var messageItem = SearchMessageItem(message, messages);
            if (messageItem == null)
            {
                return;
            }

            messageItem.CallbackMethodInfo.Invoke(messageItem.CallbackInstance, new object[] { message });
        }

        public Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
            where TMessage : IMessage
        {
            ThrowIfDisposed();

            if (!RegisteredAsyncTypes.TryGetValue(typeof(TMessage), out var messages))
            {
                return Task.CompletedTask;
            }

            var messageItem = SearchMessageItem(message, messages);
            if (messageItem == null)
            {
                return Task.CompletedTask;
            }

            var result = messageItem.CallbackMethodInfo.Invoke(messageItem.CallbackInstance, new object[] { message });

            return (Task)result;
        }

        #endregion

        #region DisposerBase

        private static void DisposeTypes(Dictionary<Type, List<MessageItem>> registeredTypes)
        {
            foreach (var pair in registeredTypes)
            {
                foreach (var item in pair.Value)
                {
                    item.Dispose();
                }
                pair.Value.Clear();
            }
            registeredTypes.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                DisposeTypes(RegisteredSyncTypes);
                DisposeTypes(RegisteredAsyncTypes);
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
