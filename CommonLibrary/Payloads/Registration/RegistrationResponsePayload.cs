using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.Registration
{
    public enum RegistrationResponseType
    {
        UserAlreadyExists,
        Successed,
        Failed,
        SmthWentWrong
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
