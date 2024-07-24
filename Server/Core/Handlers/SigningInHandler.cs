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
using System.Net;
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
                {
                    SaveSessionState(payload.Email, payload.IP);
                    responseType = SigningInResponseType.Successed;
                }
                else 
                    responseType = SigningInResponseType.Failed;
            }
            catch (Exception ex)
            {
                responseType = SigningInResponseType.SmthWentWrong;

                ExceptionHandler.HandleException(ex);
            }
        }

        private static bool UserIsFounded(string email, string password)
        {
            string commandText = "SELECT password " +
                                 "FROM users " +
                                 $"WHERE email = '{email}';";

            //Gets hash from DB
            string? hashedPassword = Convert.ToString(CommandExecutor.ExecuteScalar(commandText));

            //Means that user with such email does not exist
            if (hashedPassword is null)
                return false;

            //Hasher compares given password with the hash from DB
            return PasswordHasher.Verify(password, hashedPassword);
        }


        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new SigningInResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.SigningInResponse, response);
        }



        // ========== Session ==========
        
        private const byte LOGGED_IN = 1;

        private static void SaveSessionState(string email, string ip)
        {
            //user_id
            int user_id = GetUserId(email);

            string commandText;

            //Checks if combination of such user and device already exists
            if (SessionExists(user_id, ip))
            {
                //Updates existed session
                commandText = "UPDATE sessions " +
                              $"SET updated_at = '{DateTime.UtcNow}' " +
                              $"WHERE user_id = {user_id} " +
                                  $"AND ip = '{ip}'";
            }
            else
            {
                //Creates new session
                commandText = "INSERT INTO sessions " +
                              "(user_id, ip, status_id, updated_at) " +
                              $"VALUES({user_id}, '{ip}', {LOGGED_IN}, '{DateTime.UtcNow}')";
            }

            CommandExecutor.ExecuteNonQuery(commandText);
        }

        private static bool SessionExists(int user_id, string ip)
        {
            string commandText = "SELECT " +
                                 "CASE " +
                                     "WHEN EXISTS " +
                                     "(" +
                                         "SELECT 1 " +
                                         "FROM sessions " +
                                         $"WHERE user_id = {user_id} " +
                                             $"AND ip = '{ip}'" +
                                     ") " +
                                     "THEN 1 " +
                                     "ELSE 0 " +
                                 "END;";

            return Convert.ToBoolean(CommandExecutor.ExecuteScalar(commandText));
        }

        private static int GetUserId(string email)
        {
            string commandText = "SELECT id " +
                                 "FROM users " +
                                 $"WHERE email = '{email}'";

            return Convert.ToInt32(CommandExecutor.ExecuteScalar(commandText));
        }
    }
}
