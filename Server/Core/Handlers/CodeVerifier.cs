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
    internal class CodeVerifier : IResponsibleHandler
    {
        private static VerifyCodeResponseType responseType;

        public static void VerifyCode(ProtocolMessage message)
        {
            VerifyCodeRequestPayload payload = PayloadBuilder.GetPayload<VerifyCodeRequestPayload>(message.PayloadStream);

            if (IsCodeValid(payload.Code, payload.AssociatedUserId))
            {
                ChangeCodeState(payload.Code, payload.AssociatedUserId);

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

        private static bool IsCodeValid(string code, int userId)
        {
            string commandText = "SELECT " +
                                 "CASE " +
                                     "WHEN EXISTS " +
                                     "(" +
                                         "SELECT 1 " +
                                         "FROM recovery_codes " +
                                         $"WHERE user_id = {userId} " +
                                            $"AND code = '{code}' " +
                                            $"AND used = false " +
                                            $"AND EXTRACT(epoch FROM age('{DateTime.UtcNow}', created_at)) / 60 <= 15" +        //if code is not expired
                                     ") " +
                                     "THEN 1 " +
                                     "ELSE 0 " +
                                 "END;";

            return Convert.ToBoolean(CommandExecutor.ExecuteScalar(commandText));
        }

        private static void ChangeCodeState(string code, int userId) 
        {
            string commandText = "UPDATE recovery_codes " +
                                 "SET used = true " +
                                 $"WHERE user_id = {userId} " +
                                    $"AND code = '{code}'";

            CommandExecutor.ExecuteNonQuery(commandText);
        }
    }
}
