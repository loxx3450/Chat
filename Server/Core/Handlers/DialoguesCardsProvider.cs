using CommonLibrary;
using CommonLibrary.Models;
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

            string dialogue_name;
            string icon_path;
            bool isGroup;
            Message? last_message = null;

            while (reader.Read())
            {
                //If dialogue is not a group
                if (reader.IsDBNull(2))
                {
                    //Taking info about companion
                    dialogue_name = reader.GetString(0);
                    icon_path = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);

                    isGroup = false;
                }
                else
                {
                    //Taking info about group
                    dialogue_name = reader.GetString(2);
                    icon_path = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);

                    isGroup = true;
                }


                byte[]? iconBytes = null;

                if (!string.IsNullOrEmpty(icon_path))
                {
                    iconBytes = File.ReadAllBytes(iconsStoragePath + icon_path);
                }


                last_message = null;

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

                    last_message = new Message(message_text, sent_at, hasFiles);
                }

                dialoguesCards.Add(new DialogueCard(dialogue_name, isGroup, iconBytes, last_message));
            }

            reader.Close();
        }
    }
}
