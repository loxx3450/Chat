using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.SigningIn
{
    public class SigningInRequestPayload : JSONPayload
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public SigningInRequestPayload(string email, string password) 
        { 
            Email = email;
            Password = password;
        }
    }
}
