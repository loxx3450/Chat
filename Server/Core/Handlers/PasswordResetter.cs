using CommonLibrary;
using CommonLibrary.Payloads.ResetingPassword;
using MimeKit;
using Npgsql;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using ServerSide.Core.Services;
using ServerSide.Core.Services.DbHelpers;
using System.Security.Cryptography;

namespace ServerSide.Core.Handlers
{
    /// <summary>
    /// Sends email-message with code to reset Password
    /// </summary>
    public class PasswordResetter : IResponsibleHandler
    {
        //Response
        private static ResetPasswordResponseType responseType;
        private static int associatedUserId;

        public static void TryToSendEmail(ProtocolMessage message)
        {
            try
            {
                ResetPasswordRequestPayload payload = PayloadBuilder.GetPayload<ResetPasswordRequestPayload>(message.PayloadStream);

                if (UserDbHelper.UserExists(payload.Email))
                {
                    SendEmail(payload.Email);

                    //data for response
                    associatedUserId = UserDbHelper.GetUserId(payload.Email);
                    responseType = ResetPasswordResponseType.Success;
                }
                else 
                    responseType = ResetPasswordResponseType.Failed;
            }
            catch
            {
                responseType = ResetPasswordResponseType.SmthWentWrong;
            }
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new ResetPasswordResponsePayload(responseType, associatedUserId));

            return new SocketEventProtocolMessage(MessageType.ResetPasswordResponse, message);
        }

        private static void SendEmail(string email)
        {
            string verificationCode = VerificationCodeGenerator.GetCode();

            string subject = "Password reset";

            MimeEntity body = ConfigureMessageBody(verificationCode);

            EmailSender.SendEmail(email, subject, body);

            VerificationCodeDbHelper.SaveVerificationCode(email, verificationCode);
        }

        private static MimeEntity ConfigureMessageBody(string code)
        {
            EmailBodyConfigurator.AddHtmlBody(@"\Html\reset_password.html");
            EmailBodyConfigurator.AddImage(@"\Images\logo.png", "EmbeddedImage");
            EmailBodyConfigurator.AddSmthToHtml("&CODE", code);

            return EmailBodyConfigurator.GetMessageBody();
        }
    }
}
