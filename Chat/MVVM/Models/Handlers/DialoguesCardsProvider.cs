using Chat.Core;
using Chat.MVVM.Models.Instances;
using Chat.MVVM.Models.Services;
using Chat.MVVM.ViewModels;
using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using CommonLibrary;
using CommonLibrary.Payloads.GettingDialogues;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Handlers
{
    internal class DialoguesCardsProvider : IHandler
    {
        public static void RequestDialogues()
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new GettingDialoguesCardsRequestPayload(Client.AssociatedUserId));

            SocketEventHandler.EmitAndWait(new SocketEventProtocolMessage(MessageType.GettingDialoguesCardsRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            TransitionManager.RemoveWaiting();

            var payload = PayloadBuilder.GetPayload<GettingDialoguesCardsResponsePayload>(message.PayloadStream);

            switch (payload.ResponseType)
            {
                case GettingDialoguesCardsResponseType.Success:
                    ServiceProvider.GetRequiredService<ChatViewModel>().DialoguesCards = payload.DialoguesCards;
                    break;

                case GettingDialoguesCardsResponseType.SmthWentWrong:
                    Notifier.Notify(MessageBoxType.Error, "Something went wrong. Try again later...");
                    break;
            }
        }
    }
}
