using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Payloads.Registration;
using CommonLibrary.Payloads.SessionStateCheck;
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
    public class SessionStateChecker : IResponsibleHandler
    {
        //Will be used as a part of response
        private static SessionStateCheckResponseType responseType;

        //TODO: get user_id
        public static void Check(ProtocolMessage protocolMessage)
        {
            SessionStateCheckRequestPayload payload = PayloadBuilder.GetPayload<SessionStateCheckRequestPayload>(protocolMessage.PayloadStream);

            if (IsLoggedIn(payload.IP))
                responseType = SessionStateCheckResponseType.UserIsLoggedIn;
            else
                responseType = SessionStateCheckResponseType.UserIsLoggedOut;
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new SessionStateCheckResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.SessionStateCheckResponse, response);
        }

        private static bool IsLoggedIn(string ip)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            string cmdText = "SELECT 1 " +
                             "FROM sessions " +
                             $"WHERE ip = @ip " +
                                 $"AND EXTRACT(day FROM age(@now, updated_at)) <= 3";

            cmd.CommandText = DbHelper.FormulateBooleanRequest(cmdText);

            cmd.Parameters.AddWithValue("@ip", ip);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);

            return Convert.ToBoolean(DbHelper.ExecuteScalar(cmd));
        }
    }
}
