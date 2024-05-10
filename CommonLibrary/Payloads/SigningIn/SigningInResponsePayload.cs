using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.SigningIn
{
    public enum SigningInResponseType
    {
        Successed,
        Failed
    }

    internal class SigningInResponsePayload : JSONPayload
    {
        public SigningInResponseType ResponseType { get; set; }

        public SigningInResponsePayload(SigningInResponseType responseType)
        {
            ResponseType = responseType;
        }
    }
}
