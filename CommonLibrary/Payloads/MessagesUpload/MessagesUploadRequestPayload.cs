using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.MessagesUpload
{
    public class MessagesUploadRequestPayload : JSONPayload
    {
        public int DialogueId { get; set; }

        public MessagesUploadRequestPayload(int dialogueId)
        {
            DialogueId = dialogueId;
        }
    }
}
