using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.SigningIn
{
    public class SigningInRequestPayload : JSONPayload
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberUser { get; set; }
        public string IP {  get; set; } 

        public SigningInRequestPayload(string email, string password, bool rememberUser, string ip) 
        { 
            Email = email;
            Password = password;
            RememberUser = rememberUser;
            IP = ip;
        }
    }
}
