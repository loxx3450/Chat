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
            NpgsqlCommand cmd = new NpgsqlCommand();

            string cmdText = "SELECT 1 " +
                             "FROM recovery_codes " +
                             $"WHERE user_id = @id " +
                                 $"AND code = @code " +
                                 $"AND used = false " +
                                 $"AND EXTRACT(epoch FROM age(@now, created_at)) / 60 <= 15";           //if code is not expired

            cmd.CommandText = DbHelper.FormulateBooleanRequest(cmdText);

            cmd.Parameters.AddWithValue("@id", userId);
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);

            return Convert.ToBoolean(DbHelper.ExecuteScalar(cmd));
        }

        private static void ChangeCodeState(string code, int userId)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "UPDATE recovery_codes " +
                              "SET used = true " +
                              $"WHERE user_id = @id " +
                                  $"AND code = @code";

            cmd.Parameters.AddWithValue("@id", userId);
            cmd.Parameters.AddWithValue("@code", code);

            DbHelper.ExecuteNonQuery(cmd);
        }
    }
}
