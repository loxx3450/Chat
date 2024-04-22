using SocketEventLibrary.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    internal class ClientHandler
    {
        public ClientsCollection Clients { get; set; }

        public ClientHandler(ClientsCollection clients) 
        { 
            Clients = clients;
        }

        public void HandleClient(SocketEvent socket)
        {
            Console.WriteLine($"Client is connected at {DateTime.Now}");

            //Logic of handling clients after connection
        }
    }
}
