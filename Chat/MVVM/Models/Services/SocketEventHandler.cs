using SocketEventLibrary.SocketEventMessageCore;
using SocketEventLibrary.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Services
{
    public static class SocketEventHandler
    {
        //temp: localhost
        private const string hostname = "127.0.0.1";
        private const int port = 80;

        private static SocketEvent? socketEvent;

        public static void ConnectToServer()
        {
            ClientSocketEvent clientSocket = new ClientSocketEvent(hostname, port);

            //TODO: async plus loading on screen|Task should go away
            Task<SocketEvent> task = clientSocket.GetSocketAsync();
            socketEvent = task.Result;

            SocketEventConfigurator.ConfigurateSocketEvent(socketEvent);
        }

        public static void Emit(SocketEventMessage message)
        {
            socketEvent?.Emit(message);
        }
    }
}
