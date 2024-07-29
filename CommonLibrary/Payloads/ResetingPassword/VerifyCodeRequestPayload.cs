using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.ResetingPassword
{
    public class VerifyCodeRequestPayload : JSONPayload
    {
        public string Code { get; set; }

        public VerifyCodeRequestPayload(string code) 
        {
            Code = code;
        }
    }
}
