using Chat.Core;
using Chat.MVVM.Models.Services;
using Chat.MVVM.ViewModels;
using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using CommonLibrary;
using CommonLibrary.Payloads.MessagesUpload;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Handlers
{
    internal class MessagesProvider : IHandler
    {
        public static void RequestMessages(int dialogueId)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new MessagesUploadRequestPayload(dialogueId));

            SocketEventHandler.Emit(new SocketEventProtocolMessage(MessageType.MessagesUploadRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            var payload = PayloadBuilder.GetPayload<MessagesUploadResponsePayload>(message.PayloadStream);

            switch (payload.ResponseType)
            {
                case MessagesUploadResponseType.Success:
                    ServiceProvider.GetRequiredService<ChatViewModel>().Messages = payload.Messages;
                    break;

                case MessagesUploadResponseType.SmthWentWrong:
                    Notifier.Notify(MessageBoxType.Error, "Something went wrong. Try again later...");
                    break;
            }
        }
    }
}
