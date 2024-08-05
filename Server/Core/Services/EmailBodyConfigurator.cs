using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Services
{
    internal static class EmailBodyConfigurator
    {
        private static BodyBuilder bodyBuilder = new BodyBuilder();

        //Additional variables
        private static string storagePath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString() + "\\Storage";


        public static void AddHtmlBody(string path)
        {
            bodyBuilder.HtmlBody = File.ReadAllText(storagePath + path);
        }

        public static void AddSmthToHtml(string oldValue, string newValue)
        {
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace(oldValue, newValue);
        }

        public static void AddImage(string path, string contentId)
        {
            MimeEntity image = bodyBuilder.LinkedResources.Add(storagePath + path);
            image.ContentId = contentId;
        }

        public static MimeEntity GetMessageBody()
        {
            MimeEntity result = bodyBuilder.ToMessageBody();

            bodyBuilder = new BodyBuilder();

            return result;
        }
    }
}
