using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.Registration
{
    public class EmailVerificationRequestPayload : JSONPayload
    {
        public string Code { get; set; }
        public int AssociatedUserId { get; set; }

        public EmailVerificationRequestPayload(string code, int associatedUserId)
        {
            Code = code;
            AssociatedUserId = associatedUserId;
        }
    }
}
