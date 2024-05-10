using CommonLibrary;
using CommonLibrary.Payloads.SigningIn;
using Npgsql;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Handlers
{
    internal class SigningInHandler : IResponsibleHandler
    {
        private static SigningInResponseType responseType;

        public static void TryToSignIn(ProtocolMessage message)
        {
            try
            {
                SigningInRequestPayload payload = PayloadBuilder.GetPayload<SigningInRequestPayload>(message.PayloadStream);
                
                if (UserIsFounded(payload.Login, payload.Password)) 
                    responseType = SigningInResponseType.Successed;
                else 
                    responseType = SigningInResponseType.Failed;
            }
            catch (Exception ex) 
            { 
                responseType = SigningInResponseType.SmthWentWrong;

                ExceptionHandler.HandleException(ex);
            }
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new SigningInResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.SigningInResponse, response);
        }

        private static bool UserIsFounded(string login, string password)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.AppSettings["connString"]);

            conn.Open();

            using NpgsqlCommand cmd = new NpgsqlCommand(
                "SELECT " +
                    "CASE " +
                        "WHEN EXISTS " +
                        "( " +
                            "SELECT 1 " +
                            "FROM users " +
                            $"WHERE login = '{login}' " +
                                $"AND password = '{password}'" +
                        ") " +
                        "THEN 1 " +
                        "ELSE 0 " +
                    "END;", 
                conn);

            return Convert.ToBoolean(cmd.ExecuteScalar());
        }
    }
}
