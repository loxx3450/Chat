using CommonLibrary;
using CommonLibrary.Payloads.ResetingPassword.ChangePassword;
using Npgsql;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using ServerSide.Core.Services;
using ServerSide.Core.Services.DbHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Handlers
{
    internal class PasswordChanger : IResponsibleHandler
    {
        //Response data
        private static ChangePasswordResponseType responseType;

        public static void ChangePassword(ProtocolMessage message)
        {
            var payload = PayloadBuilder.GetPayload<ChangePasswordRequestPayload>(message.PayloadStream);

            try
            {
                UserDbHelper.UpdatePassword(payload.AssociatedUserId, PasswordHasher.Hash(payload.NewPassword));

                responseType = ChangePasswordResponseType.Success;
            }
            catch
            {
                responseType = ChangePasswordResponseType.SmthWentWrong;
            }
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new ChangePasswordResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.ChangePasswordResponse, response);
        }
    }
}
