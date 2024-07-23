using CommonLibrary;
using CommonLibrary.Payloads.SigningIn;
using Npgsql;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using ServerSide.Core.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
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
                
                if (UserIsFounded(payload.Email, payload.Password)) 
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

        private static bool UserIsFounded(string email, string password)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.AppSettings["connString"]);

            conn.Open();

            //Checks if user with such email even exists
            using NpgsqlCommand cmd = new NpgsqlCommand(
                "SELECT password " +
                "FROM users " +
                $"WHERE email = '{email}';",
                conn);

            string? hashedPassword = Convert.ToString(cmd.ExecuteScalar());

            //Means that user with such email does not exist
            if (hashedPassword is null)
                return false;

            //Hasher compares given password with the hash from DB
            return PasswordHasher.Verify(password, hashedPassword);
        }
    }
}
