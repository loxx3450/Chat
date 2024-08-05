using CommonLibrary;
using CommonLibrary.Payloads.SigningIn;
using Npgsql;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using ServerSide.Core.Services;
using ServerSide.Core.Services.DbHelpers;
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
                
                if (UserDbHelper.UserExists(payload.Email) && ArePasswordsEqual(payload.Email, payload.Password))
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

        private static bool ArePasswordsEqual(string email, string password)
        {
            //Getting hash from DB
            string? hashedPassword = UserDbHelper.GetPassword(email);

            //Hasher compares given password with the hash from DB
            return PasswordHasher.Verify(password, hashedPassword);
        }


        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new SigningInResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.SigningInResponse, response);
        }



        private static void SaveSessionState(string email, string ip)
        {
            //user_id
            int user_id = UserDbHelper.GetUserId(email);

            //Checks if combination of such user and device already exists
            if (SessionDbHelper.IsSessionFounded(user_id, ip))
            {
                SessionDbHelper.UpdateExistedSession(user_id, ip);
            }
            else
            {
                SessionDbHelper.CreateNewSession(user_id, ip);
            }
        }
    }
}
