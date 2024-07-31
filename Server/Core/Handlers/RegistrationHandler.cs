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

                if (UserDbHelper.UserExists(payload.User.Email))
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

        private static bool CreateUser(User user) 
        {
            //Generates hashed password
            string password = PasswordHasher.Hash(user.Password);

            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = "INSERT INTO users " +
                              "(username, email, password, created_at, updated_at)" +
                              $"VALUES(@username, @email, @password, @now, @now);";

            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);


            try
            {
                DbHelper.ExecuteNonQuery(cmd);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
