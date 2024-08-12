using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Payloads.Registration;
using CommonLibrary.Payloads.SessionStateCheck;
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
    public class SessionStateChecker : IResponsibleHandler
    {
        //Response data
        private static SessionStateCheckResponseType responseType;
        private static int associatedUserId;

        //TODO: get user_id
        public static void Check(ProtocolMessage protocolMessage)
        {
            var payload = PayloadBuilder.GetPayload<SessionStateCheckRequestPayload>(protocolMessage.PayloadStream);

            if (SessionDbHelper.IsActualSessionFounded(payload.IP))
            {
                associatedUserId = SessionDbHelper.GetUserId(payload.IP);

                responseType = SessionStateCheckResponseType.UserIsLoggedIn;
            }
            else
                responseType = SessionStateCheckResponseType.UserIsLoggedOut;
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new SessionStateCheckResponsePayload(responseType, associatedUserId));

            return new SocketEventProtocolMessage(MessageType.SessionStateCheckResponse, response);
        }
    }
}
