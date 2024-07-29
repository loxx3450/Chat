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
        //Response
        private static ResetPasswordResponseType responseType;

        //Additional variables
        private static string storagePath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString() + "\\Storage";
        private const int CODE_LENGTH = 6;

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

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new ResetPasswordResponsePayload(responseType));

            return new SocketEventProtocolMessage(MessageType.ResetPasswordResponse, message);
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

        private static int GetUserId(string email)
        {
            string commandText = "SELECT id " +
                                 "FROM users " +
                                 $"WHERE email = '{email}'";

            return Convert.ToInt32(CommandExecutor.ExecuteScalar(commandText));
        }

        private static void SendEmail(string email)
        {
            string recoveryCode = GetUniqueKey(CODE_LENGTH);

            string subject = "Password reset";

            MimeEntity body = GetMessageBody(recoveryCode);

            EmailSender.SendEmail(email, subject, body);

            //saving recovery code in db
            string commandText = "INSERT INTO recovery_codes(user_id, code, created_at)" +
                                 $"VALUES({GetUserId(email)}, '{recoveryCode}', '{DateTime.UtcNow}');";

            CommandExecutor.ExecuteNonQuery(commandText);
        }

        private static MimeEntity GetMessageBody(string code)
        {
            BodyBuilder bodyBuilder = new BodyBuilder();
            string htmlBody = File.ReadAllText(storagePath + @"\Html\test.html");

            //Adding image to body
            string imagePath = storagePath + @"\Images\logo.png";

            MimeEntity image = bodyBuilder.LinkedResources.Add(imagePath);
            image.ContentId = "EmbeddedImage";

            //Adding recovery code to email's body
            htmlBody = AddRecoveryCode(htmlBody, code);

            bodyBuilder.HtmlBody = htmlBody;

            return bodyBuilder.ToMessageBody();
        }

        private static string AddRecoveryCode(string htmlBody, string code)
        {
            return htmlBody.Replace("&CODE", code);
        }

        private static string GetUniqueKey(int length)
        {
            return RandomNumberGenerator.GetString("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length);
        }
    }
}
