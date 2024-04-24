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

        public Server(string hostname = "127.0.0.1", int port = 80)
        {
            serverSocket = new ServerSocketEvent(hostname, port);
        }

        public void Start()
        {
            serverSocket.StartAcceptingClients();

            serverSocket.OnClientIsConnected += ClientHandler.HandleClient;
        }

        public void Stop()
        {
            serverSocket.StopAcceptingClients();
        }
    }
}
