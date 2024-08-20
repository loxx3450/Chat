using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class DialogueCard
    {
        public string DialogueName { get; set; }

        public bool IsGroup { get; set; }

        public byte[]? IconStream { get; set; }

        public Message? LastMessage { get; set; }

        public DialogueCard(string dialogueName, bool isGroup, byte[]? iconStream = null, Message? lastMessage = null)
        {
            DialogueName = dialogueName;

            IsGroup = isGroup;

            IconStream = iconStream;

            LastMessage = lastMessage;
        }
    }
}
