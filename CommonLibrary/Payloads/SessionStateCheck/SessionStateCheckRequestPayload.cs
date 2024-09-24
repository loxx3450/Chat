using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.SessionStateCheck
{
    public class SessionStateCheckRequestPayload : JSONPayload
    {
        public string IP { get; set; }

        public SessionStateCheckRequestPayload(string ip) 
        { 
            IP = ip;
        }
    }
}
