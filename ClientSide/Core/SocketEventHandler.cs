using ClientSide.Core.Handlers;
using CommonLibrary;
using SocketEventLibrary.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Core
{
    internal class SocketEventHandler
    {
        public static void HandleSocket(SocketEvent socket)
        {
            //1. Sets supported SocketMessage's Types for income
            socket.AddSupportedMessageType<SocketEventProtocolMessage>();

            //2. Subscribes on Events from Client
            //socket.On(MessageType.)

            //3. Subscribes on service Events
            socket.OnThrowedException += ExceptionHandler.HandleException;
            socket.OnDisconnecting += DisconnectionHandler.Disconnect;
            socket.OnOtherSideIsDisconnected += BreakUpHandler.HandleBreakUp;
        }
    }
}
