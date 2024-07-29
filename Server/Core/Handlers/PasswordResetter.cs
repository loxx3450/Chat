using CommonLibrary;
using CommonLibrary.Payloads.ResetingPassword;
using MimeKit;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using ServerSide.Core.Services;
using System.Security.Cryptography;

namespace ServerSide.Core.Handlers
{
    /// <summary>
    /// Sends email-message with code to reset Password
    /// </summary>
    public class PasswordResetter : IResponsibleHandler
    {
        private static ResetPasswordResponseType responseType;
        private static string StoragePath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString() + "\\Storage";

        public static void TryToSendEmail(ProtocolMessage message)
        {
            try
            {
                ResetPasswordRequestPayload payload = PayloadBuilder.GetPayload<ResetPasswordRequestPayload>(message.PayloadStream);

                if (UserIsFounded(payload.Email))
                {
                    SendEmail(payload.Email);
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

        private static bool UserIsFounded(string email)
        {
            string commandText = "SELECT " +
                                 "CASE " +
                                     "WHEN EXISTS " +
                                     "(" +
                                         "SELECT 1 " +
                                         "FROM users " +
                                         $"WHERE email = '{email}'" +
                                     ") " +
                                     "THEN 1 " +
                                     "ELSE 0 " +
                                 "END;";

            return Convert.ToBoolean(CommandExecutor.ExecuteScalar(commandText));
        }

        private static void SendEmail(string email)
        {
            string subject = "Password reset";

            MimeEntity body = GetMessageBody();

            EmailSender.SendEmail(email, subject, body);
        }

        private static MimeEntity GetMessageBody()
        {
            BodyBuilder bodyBuilder = new BodyBuilder();
            string htmlBody = File.ReadAllText(StoragePath + @"\Html\test.html");

            //Adding image to body
            string imagePath = StoragePath + @"\Images\logo.png";

            MimeEntity image = bodyBuilder.LinkedResources.Add(imagePath);
            image.ContentId = "EmbeddedImage";

            //Adding recovery code to email's body
            htmlBody = AddRecoveryCode(htmlBody);

            bodyBuilder.HtmlBody = htmlBody;

            return bodyBuilder.ToMessageBody();
        }

        private static string AddRecoveryCode(string htmlBody)
        {
            //TODO: save in db
            return htmlBody.Replace("&CODE", GetUniqueKey(6));
        }

        private static string GetUniqueKey(int length)
        {
            return RandomNumberGenerator.GetString("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length);
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new ResetPasswordResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.ResetPasswordResponse, message);
        }
    }
}
