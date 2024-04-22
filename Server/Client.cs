using SocketEventLibrary.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    internal class Client
    {
        public SocketEvent Socket { get; set; }

        public Client(SocketEvent socket)
        {
            Socket = socket;
        }
    }
}
