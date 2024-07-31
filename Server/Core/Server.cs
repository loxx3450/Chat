using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerSide.Core.Services;
using SocketEventLibrary;
using SocketEventLibrary.Sockets;

namespace ServerSide.Core
{
    internal class Server
    {
        //Global Info
        private ServerSocketEvent serverSocket;

        public Server(string hostname = "127.0.0.1", int port = 80)
        {
            serverSocket = new ServerSocketEvent(hostname, port);

            DbHelper.OpenConnection();
        }

        public void Start()
        {
            serverSocket.StartAcceptingClients();

            serverSocket.OnClientIsConnected += ClientHandler.HandleClient;

            //Starting all service Threads
        }

        public void Stop()
        {
            serverSocket.StopAcceptingClients();

            //Interrupting all service Threads
        }

        public void StartAcceptingClients()
            => serverSocket.StartAcceptingClients();

        public void StopAcceptingClients()
            => serverSocket.StopAcceptingClients();

        ~Server()
        {
            DbHelper.CloseConnection();
        }
    }
}
