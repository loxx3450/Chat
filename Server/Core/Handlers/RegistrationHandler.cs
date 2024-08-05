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
using MimeKit;

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
                    {
                        SendEmail(payload.User.Email);

                        responseType = RegistrationResponseType.Successed;
                    }
                    else
                        responseType = RegistrationResponseType.Failed;
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

            try
            {
                //If there is an user with this email, but nou verified
                if (UserDbHelper.EmailExists(user.Email))
                {
                    UserDbHelper.CreateNewUserOnUnverifiedEmail(user.Email, user.Username, password);
                }
                else
                {
                    UserDbHelper.CreateNewUser(user.Email, user.Username, password);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void SendEmail(string email)
        {
            string verificationCode = VerificationCodeGenerator.GetCode();

            string subject = "Email verification";

            MimeEntity body = ConfigureMessageBody(verificationCode);

            EmailSender.SendEmail(email, subject, body);

            VerificationCodeDbHelper.SaveVerificationCode(email, verificationCode);
        }

        private static MimeEntity ConfigureMessageBody(string code)
        {
            EmailBodyConfigurator.AddHtmlBody(@"\Html\verify_email.html");
            EmailBodyConfigurator.AddImage(@"\Images\logo.png", "EmbeddedImage");
            EmailBodyConfigurator.AddSmthToHtml("&CODE", code);

            return EmailBodyConfigurator.GetMessageBody();
        }
    }
}
