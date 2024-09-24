using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Handlers
{
    internal interface IHandler
    {
        public static abstract void HandleResponse(ProtocolMessage message);
    }
}
