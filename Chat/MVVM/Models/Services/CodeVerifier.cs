using CommonLibrary.Payloads.ResetingPassword;
using CommonLibrary;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.MVVM.Models.Instances;

namespace Chat.MVVM.Models.Services
{
    internal class CodeVerifier
    {
        //Verify Recovery code
        public static void VerifyCode(string code)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new VerifyCodeRequestPayload(code, Client.AssociatedUserId));

            SocketEventHandler.Emit(new SocketEventProtocolMessage(MessageType.VerifyCodeRequest, message));
        }
    }
}
