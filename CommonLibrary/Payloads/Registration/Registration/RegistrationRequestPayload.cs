﻿using CommonLibrary.Models.EF;
using CommonLibrary.Payloads.PayloadTypes;
using ProtocolLibrary.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.Registration.Registration
{
    public class RegistrationRequestPayload : JSONPayload
    {
        public User User { get; set; }

        public RegistrationRequestPayload(User user)
        {
            User = user;
        }
    }
}
