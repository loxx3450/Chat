using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketEventLibrary;
using SocketEventLibrary.Sockets;

namespace ServerSide
{
    internal class Server
    {
        //Global Info
        private ServerSocketEvent serverSocket;

        //Work with clients
        private ClientsCollection clients = new ClientsCollection();
        private ClientHandler clientHandler;                                        //Maybe should be located on the level of handlers

        public Server(string hostname = "127.0.0.1", int port = 80)
        {
            serverSocket = new ServerSocketEvent(hostname, port);

            clientHandler = new ClientHandler(clients);
        }

        public void Start()
        {
            serverSocket.StartAcceptingClients();

            serverSocket.OnClientIsConnected += clientHandler.HandleClient;
        }

        public void Stop()
        {
            serverSocket.StopAcceptingClients();
        }
    }
}
