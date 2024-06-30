using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Payloads.Registration;
using ProtocolLibrary.Message;
using SocketEventLibrary.SocketEventMessageCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Services
{
    public class RegistrationService
    {
        public static void Register(User user) 
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new RegistrationRequestPayload(user));

            SocketEventHandler.Emit(new SocketEventProtocolMessage(MessageType.RegistrationRequest, message));
        }
    }
}
