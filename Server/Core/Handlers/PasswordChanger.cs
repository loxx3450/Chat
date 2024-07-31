using CommonLibrary;
using CommonLibrary.Payloads.ResetingPassword;
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
    internal class PasswordChanger : IResponsibleHandler
    {
        private static ChangePasswordResponseType responseType;

        public static void ChangePassword(ProtocolMessage message)
        {
            ChangePasswordRequestPayload payload = PayloadBuilder.GetPayload<ChangePasswordRequestPayload>(message.PayloadStream);

            try
            {
                UpdatePassword(payload.AssociatedUserId, payload.NewPassword);

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

        private static void UpdatePassword(int userId, string newPassword)
        {
            string commandText = "UPDATE users " +
                                 $"SET password = '{PasswordHasher.Hash(newPassword)}', " +
                                    $"updated_at = '{DateTime.UtcNow}' " +
                                 $"WHERE id = {userId};";

            CommandExecutor.ExecuteNonQuery(commandText);
        }
    }
}
