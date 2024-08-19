using CommonLibrary.Models;
using CommonLibrary.Payloads.PayloadTypes;
using ProtocolLibrary.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.GettingDialogues
{
    public enum GettingDialoguesCardsResponseType
    {
        Success,
        SmthWentWrong
    }

    public class GettingDialoguesCardsResponsePayload : IPayload
    {
        //State
        public GettingDialoguesCardsResponseType ResponseType { get; set; }
        public List<DialogueCard> DialoguesCards { get; set; }


        public GettingDialoguesCardsResponsePayload(GettingDialoguesCardsResponseType responseType, List<DialogueCard> dialoguesCards) 
        { 
            ResponseType = responseType;
            DialoguesCards = dialoguesCards;
        }


        // ========== Realization of IPayload ==========
        public MemoryStream GetStream()
        {
            MemoryStream memoryStream = new MemoryStream();

            using (BinaryWriter writer = new BinaryWriter(memoryStream, Encoding.UTF8, leaveOpen:true))
            {
                // ====== ResponseType ======
                writer.Write((int)ResponseType);

                foreach (DialogueCard dialogueCard in DialoguesCards)
                {
                    // ====== DialogueName ======
                    writer.Write(dialogueCard.DialogueName);


                    // ====== IconStream ======
                    if (dialogueCard.IconStream is not null)
                    {
                        writer.Write(dialogueCard.IconStream.Length);
                        writer.Write(dialogueCard.IconStream);
                    }
                    else
                    {
                        writer.Write(0);
                    }


                    // ====== LastMessage ======
                    if (dialogueCard.LastMessage is not null)
                    {
                        writer.Write(true);

                        writer.Write(dialogueCard.LastMessage.Text ?? string.Empty);
                        writer.Write(dialogueCard.LastMessage.SentAt.ToBinary());
                        writer.Write(dialogueCard.LastMessage.HasFiles);
                    }
                    else
                    {
                        writer.Write(false);
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
                var responseType = (GettingDialoguesCardsResponseType)Enum.ToObject(typeof(GettingDialoguesCardsResponseType), reader.ReadInt32());

                List<DialogueCard> dialoguesCards = new List<DialogueCard>();

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    // ====== DialogueName ======
                    string dialogueName = reader.ReadString();


                    // ====== IconStream ======
                    int iconPathLength = reader.ReadInt32();

                    //Reading IconStream if its length is not 0
                    byte[]? iconStream = iconPathLength != 0 ? reader.ReadBytes(iconPathLength) : null;


                    // ====== LastMessage ======
                    Message? lastMessage = null;

                    //If LastMessage is attached
                    if (reader.ReadBoolean())
                    {
                        string? text = reader.ReadString();

                        if (text == string.Empty)
                            text = null;

                        //Converting Long value to DateTime
                        DateTime sentAt = DateTime.FromBinary(reader.ReadInt64());

                        bool hasFiles = reader.ReadBoolean();

                        //Creating message
                        lastMessage = new Message(text, sentAt, hasFiles);
                    }

                    dialoguesCards.Add(new DialogueCard(dialogueName, iconStream, lastMessage));
                }

                return new GettingDialoguesCardsResponsePayload(responseType, dialoguesCards);
            }
        }
    }
}
