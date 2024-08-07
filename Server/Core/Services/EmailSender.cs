using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Services
{
    internal static class EmailSender
    {
        private static string SenderName = ConfigurationManager.AppSettings["appName"];
        private static string SenderEmail = ConfigurationManager.AppSettings["email"];
        private static string Password = ConfigurationManager.AppSettings["emailPass"];

        public static void SendEmail(string receiverEmail, string subject, MimeEntity body, string receiverName = "")
        {
            //Setting email data
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(SenderName, SenderEmail));
            message.To.Add(new MailboxAddress(receiverName, receiverEmail));
            message.Subject = subject;
            message.Body = body;

            //Sending email
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(SenderEmail, Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
