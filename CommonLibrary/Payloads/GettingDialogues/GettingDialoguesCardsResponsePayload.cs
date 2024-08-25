using CommonLibrary.Models.Custom;
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
                    // ====== DialogueId ======
                    writer.Write(dialogueCard.DialogueId);


                    // ====== DialogueName ======
                    writer.Write(dialogueCard.DialogueName);


                    // ====== IsGroup ======
                    writer.Write(dialogueCard.IsGroup);


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
                    if (dialogueCard.LastMessageInfo is not null)
                    {
                        writer.Write(true);

                        writer.Write(dialogueCard.LastMessageInfo.Text ?? string.Empty);
                        writer.Write(dialogueCard.LastMessageInfo.SentAt.ToBinary());
                        writer.Write(dialogueCard.LastMessageInfo.HasFiles);
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
                    // ====== DialogueId ======
                    int dialogueId = reader.ReadInt32();


                    // ====== DialogueName ======
                    string dialogueName = reader.ReadString();


                    //====== IsGroup ======
                    bool isGroup = reader.ReadBoolean();


                    // ====== IconStream ======
                    int iconPathLength = reader.ReadInt32();

                    //Reading IconStream if its length is not 0
                    byte[]? iconStream = iconPathLength != 0 ? reader.ReadBytes(iconPathLength) : null;


                    // ====== LastMessage ======
                    MessageInfo? lastMessageInfo = null;

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
                        lastMessageInfo = new MessageInfo(text, sentAt, hasFiles);
                    }

                    dialoguesCards.Add(new DialogueCard(dialogueId, dialogueName, isGroup, iconStream, lastMessageInfo));
                }

                return new GettingDialoguesCardsResponsePayload(responseType, dialoguesCards);
            }
        }
    }
}
