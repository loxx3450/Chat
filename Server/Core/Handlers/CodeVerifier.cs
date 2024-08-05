using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Payloads.ResetingPassword;
using Npgsql;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using ServerSide.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Handlers
{
    internal class CodeVerifier : IResponsibleHandler
    {
        private static VerifyCodeResponseType responseType;

        public static void VerifyCode(ProtocolMessage message)
        {
            VerifyCodeRequestPayload payload = PayloadBuilder.GetPayload<VerifyCodeRequestPayload>(message.PayloadStream);

            if (VerificationCodeDbHelper.IsCodeValid(payload.AssociatedUserId, payload.Code))
            {
                VerificationCodeDbHelper.ChangeCodeStateToUsed(payload.AssociatedUserId, payload.Code);

                responseType = VerifyCodeResponseType.Success;
            }
            else
                responseType = VerifyCodeResponseType.Failed;
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new VerifyCodeResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.VerifyCodeResponse, response);
        }
    }
}
