using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.ResetingPassword
{
    public class ResetPasswordRequestPayload : JSONPayload 
    {
        public string Email { get; set; }

        public ResetPasswordRequestPayload(string email) 
        { 
            Email = email;
        }
    }
}
