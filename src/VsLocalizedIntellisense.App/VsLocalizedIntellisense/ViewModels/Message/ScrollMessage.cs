using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsLocalizedIntellisense.Models.Mvvm.Message;

namespace VsLocalizedIntellisense.ViewModels.Message
{
    public class ScrollMessage : IMessage
    {
        public ScrollMessage(string messageId = "")
        {
            MessageId = messageId;
        }

        #region IMessage

        public string MessageId { get; set; }

        #endregion
    }
}
