using CommonLibrary;
using ServerSide.Core.Handlers;
using ServerSide.Core.Static;
using SocketEventLibrary.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core
{
    internal class ClientHandler
    {
        public static void HandleClient(SocketEvent socket)
        {
            Console.WriteLine($"Client is connected at {DateTime.Now}");

            //Logic of handling clients after connection
            Client client = new Client(socket);

            //0.
            ClientsCollection.AddClient(client);

            //1. Sets supported SocketMessage's Types for income
            socket.AddSupportedMessageType<SocketEventProtocolMessage>();

            //2. Subscribes on Events from Client
            //socket.On(MessageType.)

            //3. Subscribes on service Events
            socket.OnThrowedException += ExceptionHandler.HandleException;
            socket.OnDisconnecting += () => DisconnectionHandler.Disconnect(client);
            socket.OnOtherSideIsDisconnected += () => BreakUpHandler.HandleBreakUp(client);
        }
    }
}
