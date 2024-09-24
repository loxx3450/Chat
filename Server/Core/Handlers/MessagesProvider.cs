using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Models.Custom;
using CommonLibrary.Models.EF;
using CommonLibrary.Payloads.MessagesUpload;
using Microsoft.EntityFrameworkCore;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Handlers
{
    internal class MessagesProvider : IResponsibleHandler
    {
        //Response data
        private static List<MessageDto> messages = new List<MessageDto>();
        private static MessagesUploadResponseType responseType;

        //Additional info
        private static string imagesStoragePath = ConfigurationManager.AppSettings["storagePath"] + @"Users\Images\";

        public static void FormulateListOfMessages(ProtocolMessage message)
        {
            try
            {
                var payload = PayloadBuilder.GetPayload<MessagesUploadRequestPayload>(message.PayloadStream);

                messages = GetMessages(payload.DialogueId);

                responseType = MessagesUploadResponseType.Success;
            }
            catch
            {
                responseType = MessagesUploadResponseType.SmthWentWrong;
            }
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new MessagesUploadResponsePayload(responseType, messages));

            return new SocketEventProtocolMessage(MessageType.MessagesUploadResponse, response);
        }


        private static List<MessageDto> GetMessages(int dialogueId)
        {
            using ChatDbContext dbContext = new ChatDbContext();

            List<Message> messages = dbContext.Messages
                                              .Include(mes => mes.Files)
                                              .Where(mes => mes.DialogueId == dialogueId)
                                              .OrderByDescending(mes => mes.SentAt)
                                              .Take(50)
                                              .ToList();

            List<MessageDto> result = new List<MessageDto>();

            foreach (var message in messages)
            {
                List<FileDto>? files = null;

                foreach (var file in message.Files)
                {
                    if (files is null)
                        files = new List<FileDto>();

                    files.Add(new FileDto(file.Id, System.IO.File.ReadAllBytes(imagesStoragePath + file.FilePath), file.FileName));
                }

                result.Add(new MessageDto(message.Id, message.UserId, message.SentAt, message.Text, files));
            }

            return result;
        }
    }
}
