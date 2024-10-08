﻿using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.ResetingPassword.ResetPassword
{
    public enum ResetPasswordResponseType
    {
        Success,
        Failed,
        SmthWentWrong
    }

    public class ResetPasswordResponsePayload : JSONPayload
    {
        public ResetPasswordResponseType ResponseType { get; set; }
        public int AssociatedUserId { get; set; }

        public ResetPasswordResponsePayload(ResetPasswordResponseType responseType, int associatedUserId)
        {
            ResponseType = responseType;
            AssociatedUserId = associatedUserId;
        }
    }
}
