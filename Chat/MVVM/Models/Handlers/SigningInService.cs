﻿using Chat.Core;
using CommonLibrary;
using CommonLibrary.Payloads.SigningIn;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using CommonLibrary.Payloads.Registration;
using Chat.MVVM.ViewModels;
using System.Net;
using Chat.MVVM.Models.Services;
using Chat.MVVM.Models.Instances;

namespace Chat.MVVM.Models.Handlers
{
    public class SigningInService : IHandler
    {
        public static void SignIn(string email, string password, bool rememberUser)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new SigningInRequestPayload(email, password, rememberUser, IPAddressFetcher.GetIPAddress().ToString()));

            SocketEventHandler.EmitAndWait(new SocketEventProtocolMessage(MessageType.SigningInRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            TransitionManager.RemoveWaiting();

            var payload = PayloadBuilder.GetPayload<SigningInResponsePayload>(message.PayloadStream);

            switch (payload.ResponseType)
            {
                case SigningInResponseType.Successed:
                    Client.AssociatedUserId = payload.AssociatedUserId;
                    Navigator.NavigateTo<ChatViewModel>();
                    break;

                case SigningInResponseType.Failed:
                    Notifier.Notify(MessageBoxType.Error, "Your data is invalid :(");
                    break;

                case SigningInResponseType.SmthWentWrong:
                    Notifier.Notify(MessageBoxType.Error, "Something went wrong. Try again later...");
                    break;
            }
        }
    }
}
