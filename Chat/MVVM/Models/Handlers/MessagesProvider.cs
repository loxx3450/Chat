using Chat.MVVM.Models.Services;
using CommonLibrary;
using CommonLibrary.Payloads.MessagesUpload;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Handlers
{
    internal class MessagesProvider : IHandler
    {
        public static void RequestMessages(int dialogueId)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new MessagesUploadRequestPayload(dialogueId));

            SocketEventHandler.Emit(new SocketEventProtocolMessage(MessageType.MessagesUploadRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
