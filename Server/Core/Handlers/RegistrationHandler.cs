using CommonLibrary.Models;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using ServerSide.Core.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;
using CommonLibrary;
using CommonLibrary.Payloads.Registration;
using System.Security.Cryptography;
using ServerSide.Core.Services;

namespace ServerSide.Core.Handlers
{

    internal class RegistrationHandler : IResponsibleHandler
    {
        //Will be used as a part of response
        private static RegistrationResponseType responseType;


        public static void TryToCreateUser(ProtocolMessage protocolMessage)
        {
            //analyzing protocol Message
            try
            {
                RegistrationRequestPayload payload = PayloadBuilder.GetPayload<RegistrationRequestPayload>(protocolMessage.PayloadStream);

                if (DoesAlreadyExist(payload.User))
                {
                    responseType = RegistrationResponseType.UserAlreadyExists;
                }
                else
                {
                    if (CreateUser(payload.User))
                        responseType = RegistrationResponseType.Successed;
                    else
                        responseType = RegistrationResponseType.Failed;                         //TODO?: get errorMessage
                }
            }
            catch
            {
                responseType = RegistrationResponseType.SmthWentWrong;
            }
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new RegistrationResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.RegistrationResponse, response);
        }


        private static bool DoesAlreadyExist(User user)
        {
            string commandText = "SELECT " +
                                 "CASE " +
                                     "WHEN EXISTS " +
                                     "(" +
                                         "SELECT 1 " +
                                         "FROM users " +
                                         $"WHERE email = '{user.Email}'" +
                                     ") " +
                                     "THEN 1 " +
                                     "ELSE 0 " +
                                 "END;";

            return Convert.ToBoolean(CommandExecutor.ExecuteScalar(commandText));
        }

        private static bool CreateUser(User user) 
        {
            //Generates hashed password
            string password = PasswordHasher.Hash(user.Password);

            string commandText = "INSERT INTO users " +
                                 "(username, email, password, created_at, updated_at)" +
                                 $"VALUES('{user.Username}', '{user.Email}', '{password}', '{DateTime.UtcNow}', '{DateTime.UtcNow}');";

            try
            {
                CommandExecutor.ExecuteNonQuery(commandText);

                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
