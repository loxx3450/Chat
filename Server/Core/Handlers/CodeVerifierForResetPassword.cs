﻿using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Payloads.ResetingPassword.VerifyCode;
using Npgsql;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using ServerSide.Core.Services.DbHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Handlers
{
    internal class CodeVerifierForResetPassword : IResponsibleHandler
    {
        //Response data
        private static VerifyCodeForResetPasswordResponseType responseType;

        public static void VerifyCode(ProtocolMessage message)
        {
            var payload = PayloadBuilder.GetPayload<VerifyCodeForResetPasswordRequestPayload>(message.PayloadStream);

            if (VerificationCodeDbHelper.IsCodeValid(payload.AssociatedUserId, payload.Code))
            {
                VerificationCodeDbHelper.ChangeCodeStateToUsed(payload.AssociatedUserId, payload.Code);

                responseType = VerifyCodeForResetPasswordResponseType.Success;
            }
            else
                responseType = VerifyCodeForResetPasswordResponseType.Failed;
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new VerifyCodeForResetPasswordResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.VerifyCodeForResetPasswordResponse, response);
        }
    }
}
