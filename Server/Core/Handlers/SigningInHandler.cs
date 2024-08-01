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
                    if (payload.RememberUser)
                        SaveSessionState(payload.Email, payload.IP);

                    responseType = SigningInResponseType.Successed;
                }
                else 
                    responseType = SigningInResponseType.Failed;
            }
            catch
            {
                responseType = SigningInResponseType.SmthWentWrong;
            }
        }

        private static bool UserIsFounded(string email, string password)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "SELECT password " +
                              "FROM users " +
                              $"WHERE email = @email;";

            cmd.Parameters.AddWithValue("@email", email);

            //Gets hash from DB
            string? hashedPassword = Convert.ToString(DbHelper.ExecuteScalar(cmd));

            //Means that user with such email does not exist
            if (string.IsNullOrEmpty(hashedPassword))
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
            int user_id = UserDbHelper.GetUserId(email);

            NpgsqlCommand cmd = new NpgsqlCommand();

            //Checks if combination of such user and device already exists
            if (SessionExists(user_id, ip))
            {
                //Updates existed session
                cmd.CommandText = "UPDATE sessions " +
                                  $"SET updated_at = @now " +
                                  $"WHERE user_id = @id " +
                                      $"AND ip = @ip;";

                cmd.Parameters.AddWithValue("@id", user_id);
                cmd.Parameters.AddWithValue("@ip", ip);
                cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            }
            else
            {
                //Creates new session
                cmd.CommandText = "INSERT INTO sessions " +
                                  "(user_id, ip, status_id, updated_at) " +
                                  $"VALUES(@id, @ip, @loggedIn, @now);";

                cmd.Parameters.AddWithValue("@id", user_id);
                cmd.Parameters.AddWithValue("@ip", ip);
                cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@loggedIn", LOGGED_IN);
            }

            DbHelper.ExecuteNonQuery(cmd);
        }

        private static bool SessionExists(int user_id, string ip)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();

            string cmdText = "SELECT 1 " +
                             "FROM sessions " +
                             $"WHERE user_id = @id " +
                                 $"AND ip = @ip";

            cmd.CommandText = DbHelper.FormulateBooleanRequest(cmdText);

            cmd.Parameters.AddWithValue("@id", user_id);
            cmd.Parameters.AddWithValue("@ip", ip);

            return Convert.ToBoolean(DbHelper.ExecuteScalar(cmd));
        }
    }
}
