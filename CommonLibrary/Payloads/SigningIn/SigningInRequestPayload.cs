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
        public string Login { get; set; }
        public string Password { get; set; }

        public SigningInRequestPayload(string login, string password) 
        { 
            Login = login;
            Password = password;
        }
    }
}
