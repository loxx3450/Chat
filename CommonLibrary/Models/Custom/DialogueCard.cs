using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models.Custom
{
    public class DialogueCard
    {
        public int DialogueId { get; set; }

        public string DialogueName { get; set; }

        public bool IsGroup { get; set; }

        public byte[]? IconStream { get; set; }

        public MessageInfo? LastMessageInfo { get; set; }


        public DialogueCard(int dialogueId, string dialogueName, bool isGroup, byte[]? iconStream = null, MessageInfo? lastMessageInfo = null)
        {
            DialogueId = dialogueId;

            DialogueName = dialogueName;

            IsGroup = isGroup;

            IconStream = iconStream;

            LastMessageInfo = lastMessageInfo;
        }
    }
}
