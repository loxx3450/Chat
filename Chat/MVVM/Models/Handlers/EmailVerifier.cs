using Chat.MVVM.Models.Instances;
using Chat.MVVM.Models.Services;
using CommonLibrary;
using CommonLibrary.Payloads.Registration;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Handlers
{
    internal class EmailVerifier : IHandler
    {
        public static void Verify(string code)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new EmailVerificationRequestPayload(code, Client.AssociatedUserId));

            SocketEventHandler.EmitAndWait(new SocketEventProtocolMessage(MessageType.EmailVerificationRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
