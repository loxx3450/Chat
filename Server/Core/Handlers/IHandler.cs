using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Handlers
{
    //Handler that is supposed to generate Response
    internal interface IResponsibleHandler
    {
        abstract static SocketEventProtocolMessage GetResponse();
    }
}
