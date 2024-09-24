using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Handlers
{
    internal class DisconnectionHandler : IResponsibleHandler
    {
        public static void Disconnect(Client client)
        {
            //Logic of disconnecting
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            throw new NotImplementedException();
        }
    }
}
