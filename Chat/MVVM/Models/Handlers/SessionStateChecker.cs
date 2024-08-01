using Chat.MVVM.Models.Services;
using Chat.MVVM.ViewModels;
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
    public static class SessionStateChecker
    {
        public static void CheckState()
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new SessionStateCheckRequestPayload(IPAddressFetcher.GetIPAddress().ToString()));

            SocketEventHandler.Emit(new SocketEventProtocolMessage(MessageType.SessionStateCheckRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            SessionStateCheckResponsePayload payload = PayloadBuilder.GetPayload<SessionStateCheckResponsePayload>(message.PayloadStream);

            switch (payload.ResponseType)
            {
                case SessionStateCheckResponseType.UserIsLoggedIn:
                    Navigator.NavigateTo<ChatViewModel>();
                    break;

                case SessionStateCheckResponseType.UserIsLoggedOut:
                    Navigator.NavigateTo<LoginViewModel>();
                    break;
            }
        }
    }
}
