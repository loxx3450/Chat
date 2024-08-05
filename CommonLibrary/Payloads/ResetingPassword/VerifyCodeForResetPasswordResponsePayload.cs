using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.ResetingPassword
{
    public enum VerifyCodeForResetPasswordResponseType
    {
        Success,
        Failed
    }

    public class VerifyCodeForResetPasswordResponsePayload : JSONPayload
    {
        public VerifyCodeForResetPasswordResponseType ResponseType { get; set; }

        public VerifyCodeForResetPasswordResponsePayload(VerifyCodeForResetPasswordResponseType responseType)
        {
            ResponseType = responseType;
        }
    }
}
