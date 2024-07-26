using CommonLibrary;
using CommonLibrary.Models;
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
    /// <summary>
    /// Sends email-message with code to reset Password
    /// </summary>
    public class PasswordResetter : IResponsibleHandler
    {
        private static ResetPasswordResponseType responseType;

        public static void TryToSendEmail(ProtocolMessage message)
        {
            try
            {
                ResetPasswordRequestPayload payload = PayloadBuilder.GetPayload<ResetPasswordRequestPayload>(message.PayloadStream);

                if (UserIsFounded(payload.Email))
                {
                    SendEmail(payload.Email);
                    responseType = ResetPasswordResponseType.Success;
                }
                else 
                    responseType = ResetPasswordResponseType.Failed;
            }
            catch
            {
                responseType = ResetPasswordResponseType.SmthWentWrong;
            }
        }

        private static bool UserIsFounded(string email)
        {
            string commandText = "SELECT " +
                                 "CASE " +
                                     "WHEN EXISTS " +
                                     "(" +
                                         "SELECT 1 " +
                                         "FROM users " +
                                         $"WHERE email = '{email}'" +
                                     ") " +
                                     "THEN 1 " +
                                     "ELSE 0 " +
                                 "END;";

            return Convert.ToBoolean(CommandExecutor.ExecuteScalar(commandText));
        }

        private static void SendEmail(string email)
        {
            //Logic of sending email
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new ResetPasswordResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.ResetPasswordResponse, message);
        }
    }
}
