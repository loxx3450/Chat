using CommonLibrary.Models.Custom;
using CommonLibrary.Models.EF;
using CommonLibrary.Payloads.PayloadTypes;
using ProtocolLibrary.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.MessagesUpload
{
    public enum MessagesUploadResponseType
    {
        Success,
        SmthWentWrong
    }


    public class MessagesUploadResponsePayload : IPayload
    {
        public MessagesUploadResponseType ResponseType { get; set; }
        public List<MessageDto> Messages { get; set; }

        public MessagesUploadResponsePayload(MessagesUploadResponseType responseType, List<MessageDto> messages)
        {
            ResponseType = responseType;
            Messages = messages;
        }

        public MemoryStream GetStream()
        {
            MemoryStream memoryStream = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
            {
                // ====== ResponseType ======
                writer.Write((int)ResponseType);

                foreach (MessageDto message in Messages)
                {
                    // ====== MessageId ======
                    writer.Write(message.Id);


                    // ====== UserId ======
                    writer.Write(message.UserId);


                    // ====== SentAt ======
                    writer.Write(message.SentAt.ToBinary());


                    // ====== Text ======
                    if (message.Text is not null)
                        writer.Write(message.Text);
                    else
                        writer.Write(string.Empty);


                    // ====== Files ======
                    if (message.Files is not null)
                    {
                        writer.Write(message.Files.Count);

                        foreach (FileDto file in message.Files)
                        {
                            // ====== FileId ======
                            writer.Write(file.Id);


                            // ====== FileName ======
                            if (file.FileName is not null)
                                writer.Write(file.FileName);
                            else
                                writer.Write(string.Empty);


                            // ====== FileContent ======
                            writer.Write(file.FileContent.Length);
                            writer.Write(file.FileContent);
                        }
                    }
                    else
                    {
                        writer.Write(0);
                    }
                }

                writer.Flush();
            }

            memoryStream.Position = 0;

            return memoryStream;
        }

        public static object GetPayload(MemoryStream memoryStream, Type returnType)
        {
            memoryStream.Position = 0;

            using (BinaryReader reader = new BinaryReader(memoryStream, Encoding.UTF8))
            {
                // ====== ResponseType ======
                var responseType = (MessagesUploadResponseType)Enum.ToObject(typeof(MessagesUploadResponseType), reader.ReadInt32());

                List<MessageDto> messages = new List<MessageDto>();

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    // ====== MessageId ======
                    int messageId = reader.ReadInt32();


                    // ====== UserId ======
                    int userId = reader.ReadInt32();


                    // ====== SentAt ======
                    DateTime sentAt = DateTime.FromBinary(reader.ReadInt64());


                    // ====== Text ======
                    string? text = reader.ReadString();

                    if (text == string.Empty)
                        text = null;


                    int filesCount = reader.ReadInt32();

                    if (filesCount > 0)
                    {
                        // ====== Files ======
                        List<FileDto> files = new List<FileDto>();

                        for (int i = 0; i < filesCount; ++i)
                        {
                            // ====== FileId ======
                            int fileId = reader.ReadInt32();


                            // ====== FileName ======
                            string? fileName = reader.ReadString();
                        
                            if (fileName == string.Empty)
                                fileName = null;


                            // ====== FileContent ======
                            int fileContentLength = reader.ReadInt32();

                            byte[] fileContent = reader.ReadBytes(fileContentLength);


                            files.Add(new FileDto(fileId, fileContent, fileName));
                        }

                        messages.Add(new MessageDto(messageId, userId, sentAt, text, files));
                    }
                    else
                    {
                        messages.Add(new MessageDto(messageId, userId, sentAt, text));
                    }
                }

                return new MessagesUploadResponsePayload(responseType, messages);
            }
        }
    }
}
