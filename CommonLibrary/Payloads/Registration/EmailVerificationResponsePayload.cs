using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.Registration
{
    public enum EmailVerificationResponseType
    {
        Success,
        Failed,
        SmthWentWrong
    }

    public class EmailVerificationResponsePayload : JSONPayload
    {
        public EmailVerificationResponseType ResponseType { get; set; }

        public EmailVerificationResponsePayload(EmailVerificationResponseType responseType)
        {
            ResponseType = responseType;
        }
    }
}
