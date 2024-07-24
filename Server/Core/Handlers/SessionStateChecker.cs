using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Payloads.Registration;
using CommonLibrary.Payloads.SessionStateCheck;
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

        private static bool IsLoggedIn(string ip)
        {
            string commandText = "SELECT " +
                                 "CASE " +
                                     "WHEN EXISTS " +
                                     "(" +
                                         "SELECT 1 " +
                                         "FROM sessions " +
                                         $"WHERE ip = '{ip}' " +
                                             $"AND EXTRACT(day FROM age(TIMESTAMP '{DateTime.UtcNow}', updated_at)) <= 3" +
                                     ") " +
                                     "THEN 1 " +
                                     "ELSE 0 " +
                                 "END;";

            return Convert.ToBoolean(CommandExecutor.ExecuteScalar(commandText));
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new SessionStateCheckResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.SessionStateCheckResponse, response);
        }
    }
}
