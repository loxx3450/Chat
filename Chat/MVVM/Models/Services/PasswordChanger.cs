using Chat.MVVM.Models.Instances;
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
    internal class PasswordChanger
    {
        public static void ChangePassword(string newPassword)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new ChangePasswordRequestPayload(newPassword, Client.AssociatedUserId));

            SocketEventHandler.Emit(new SocketEventProtocolMessage(MessageType.ChangePasswordRequest, message));
        }
    }
}
