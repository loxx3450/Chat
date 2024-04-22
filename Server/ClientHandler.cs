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
            //Logic of handling clients after connection
        }
    }
}
