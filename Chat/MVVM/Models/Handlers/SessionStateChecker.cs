using Chat.MVVM.Models.Instances;
using Chat.MVVM.Models.Services;
using Chat.MVVM.ViewModels;
using Chat.MVVM.ViewModels.EntryWindows;
using CommonLibrary;
using CommonLibrary.Payloads.SessionStateCheck;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Handlers
{
    public class SessionStateChecker : IHandler
    {
        public static void CheckState()
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new SessionStateCheckRequestPayload(IPAddressFetcher.GetIPAddress().ToString()));

            SocketEventHandler.EmitAndWait(new SocketEventProtocolMessage(MessageType.SessionStateCheckRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            TransitionManager.RemoveWaiting();

            var payload = PayloadBuilder.GetPayload<SessionStateCheckResponsePayload>(message.PayloadStream);

            switch (payload.ResponseType)
            {
                case SessionStateCheckResponseType.UserIsLoggedIn:
                    Client.AssociatedUserId = payload.AssociatedUserId;
                    Navigator.NavigateTo<ChatViewModel>();
                    break;

                case SessionStateCheckResponseType.UserIsLoggedOut:
                    Navigator.NavigateTo<LoginViewModel>();
                    break;
            }
        }
    }
}
