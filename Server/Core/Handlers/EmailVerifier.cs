using CommonLibrary;
using CommonLibrary.Payloads.Registration;
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
    internal class EmailVerifier : IResponsibleHandler
    {
        private static EmailVerificationResponseType responseType;

        public static void Verify(ProtocolMessage message)
        {
            EmailVerificationRequestPayload payload = PayloadBuilder.GetPayload<EmailVerificationRequestPayload>(message.PayloadStream);

            try
            {
                if (VerificationCodeDbHelper.IsCodeValid(payload.AssociatedUserId, payload.Code))
                {
                    VerificationCodeDbHelper.ChangeCodeStateToUsed(payload.AssociatedUserId, payload.Code);

                    UserDbHelper.MarkUserAsVerified(payload.AssociatedUserId);

                    responseType = EmailVerificationResponseType.Success;
                }
                else
                {
                    responseType = EmailVerificationResponseType.Failed;
                }
            }
            catch
            {
                responseType = EmailVerificationResponseType.SmthWentWrong;
            }
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new EmailVerificationResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.EmailVerificationResponse, response);
        }
    }
}
