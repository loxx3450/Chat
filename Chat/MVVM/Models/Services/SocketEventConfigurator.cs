using CommonLibrary;
using ProtocolLibrary.Message;
using SocketEventLibrary.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Services
{
    public class SocketEventConfigurator
    {
        public static void ConfigurateSocketEvent(SocketEvent socket)
        {
            //1. Sets supported SocketMessage's Types for income
            socket.AddSupportedMessageType<SocketEventProtocolMessage>();

            //2. Subscribes on Events from Client
            //TODO:
            //
            //
            socket.On(MessageType.RegistrationResponse, 
                (mes) => RegistrationService.HandleResponse((ProtocolMessage)mes));

            //3. Subscribes on service Events
            //TODO:
            //
            //
        }
    }
}
