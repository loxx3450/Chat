using CommonLibrary;
using CommonLibrary.Payloads.ResetingPassword;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Services
{
    public static class PasswordResetter
    {
        public static void ResetPassword(string email)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new ResetPasswordRequestPayload(email));

            SocketEventHandler.Emit(new SocketEventProtocolMessage(MessageType.ResetPasswordRequest, message));
        }
    }
}
