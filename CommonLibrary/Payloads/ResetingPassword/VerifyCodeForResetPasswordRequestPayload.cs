using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.ResetingPassword
{
    public class VerifyCodeForResetPasswordRequestPayload : JSONPayload
    {
        public string Code { get; set; }
        public int AssociatedUserId { get; set; }

        public VerifyCodeForResetPasswordRequestPayload(string code, int associatedUserId) 
        {
            Code = code;
            AssociatedUserId = associatedUserId;
        }
    }
}
