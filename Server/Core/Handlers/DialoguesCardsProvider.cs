using CommonLibrary;
using CommonLibrary.Models.Custom;
using CommonLibrary.Models.EF;
using CommonLibrary.Payloads.GettingDialogues;
using Npgsql;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using ServerSide.Core.Services.DbHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = System.IO.File;

namespace ServerSide.Core.Handlers
{
    internal class DialoguesCardsProvider : IResponsibleHandler
    {
        //Response data
        private static GettingDialoguesCardsResponseType responseType;
        private static List<DialogueCard> dialoguesCards = null!;

        //Additional info
        private static string iconsStoragePath = ConfigurationManager.AppSettings["storagePath"] + @"Users\Icons\";

        public static void FormulateListOfDialogues(ProtocolMessage message)
        {
            try
            {
                var payload = PayloadBuilder.GetPayload<GettingDialoguesCardsRequestPayload>(message.PayloadStream);


                //Calling DB function
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM get_dialogues_cards(@user_id)");

                cmd.Parameters.AddWithValue("@user_id", payload.UserId);

                //Formulating list of DialoguesCards
                ParseDataReader(DbHelper.ExecuteReader(cmd));


                responseType = GettingDialoguesCardsResponseType.Success;
            }
            catch
            {
                responseType = GettingDialoguesCardsResponseType.SmthWentWrong;
            }
        }

        public static SocketEventProtocolMessage GetResponse()
        {
            ProtocolMessage response = new ProtocolMessage();
            response.SetPayload(new GettingDialoguesCardsResponsePayload(responseType, dialoguesCards));

            return new SocketEventProtocolMessage(MessageType.GettingDialoguesCardsResponse, response);
        }


        private static void ParseDataReader(NpgsqlDataReader reader)
        {
            dialoguesCards = new List<DialogueCard>();

            string dialogueName;
            string iconPath;
            bool isGroup;
            MessageInfo? lastMessageInfo = null;

            while (reader.Read())
            {
                //If dialogue is not a group
                if (reader.IsDBNull(2))
                {
                    //Taking info about companion
                    dialogueName = reader.GetString(0);
                    iconPath = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);

                    isGroup = false;
                }
                else
                {
                    //Taking info about group
                    dialogueName = reader.GetString(2);
                    iconPath = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);

                    isGroup = true;
                }


                byte[]? iconBytes = null;

                if (!string.IsNullOrEmpty(iconPath))
                {
                    iconBytes = File.ReadAllBytes(iconsStoragePath + iconPath);
                }


                lastMessageInfo = null;

                //If message is attached
                if (!reader.IsDBNull(4))
                {
                    string? message_text = null;
                    DateTime sent_at = reader.GetDateTime(4);
                    bool hasFiles = reader.GetBoolean(6);

                    //If message contains some text
                    if (!reader.IsDBNull(5))
                    {
                        message_text = reader.GetString(5);
                    }

                    lastMessageInfo = new MessageInfo(message_text, sent_at, hasFiles);
                }

                dialoguesCards.Add(new DialogueCard(dialogueName, isGroup, iconBytes, lastMessageInfo));
            }

            reader.Close();
        }
    }
}
