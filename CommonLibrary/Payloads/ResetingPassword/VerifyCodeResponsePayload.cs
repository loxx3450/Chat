using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.ResetingPassword
{
    public enum VerifyCodeResponseType
    {
        Success,
        Failed
    }

    public class VerifyCodeResponsePayload : JSONPayload
    {
        public VerifyCodeResponseType ResponseType { get; set; }

        public VerifyCodeResponsePayload(VerifyCodeResponseType responseType)
        {
            ResponseType = responseType;
        }
    }
}
