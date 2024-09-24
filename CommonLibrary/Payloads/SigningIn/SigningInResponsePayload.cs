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
        Failed,
        SmthWentWrong
    }

    public class SigningInResponsePayload : JSONPayload
    {
        public SigningInResponseType ResponseType { get; set; }
        public int AssociatedUserId { get; set; }

        public SigningInResponsePayload(SigningInResponseType responseType, int associatedUserId)
        {
            ResponseType = responseType;
            AssociatedUserId = associatedUserId;
        }
    }
}
