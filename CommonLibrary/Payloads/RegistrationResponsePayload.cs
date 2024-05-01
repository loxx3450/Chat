using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads
{
    public enum RegistrationResponseType
    {
        UserAlreadyExists,
        Successed,
        Failed
    }

    public class RegistrationResponsePayload : JSONPayload
    {
        public RegistrationResponseType ResponseType { get; set; }

        public RegistrationResponsePayload(RegistrationResponseType responseType) 
        { 
            ResponseType = responseType;
        }
    }
}
