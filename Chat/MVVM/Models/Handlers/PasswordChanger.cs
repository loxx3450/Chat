﻿using Chat.MVVM.Models.Instances;
using Chat.MVVM.Models.Services;
using Chat.MVVM.ViewModels;
using Chat.MVVM.ViewModels.EntryWindows;
using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using CommonLibrary;
using CommonLibrary.Payloads.ResetingPassword.ChangePassword;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Handlers
{
    internal class PasswordChanger : IHandler
    {
        public static void ChangePassword(string newPassword)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new ChangePasswordRequestPayload(newPassword, Client.AssociatedUserId));

            SocketEventHandler.EmitAndWait(new SocketEventProtocolMessage(MessageType.ChangePasswordRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            TransitionManager.RemoveWaiting();

            var payload = PayloadBuilder.GetPayload<ChangePasswordResponsePayload>(message.PayloadStream);

            switch (payload.ResponseType)
            {
                case ChangePasswordResponseType.Success:
                    Notifier.Notify(MessageBoxType.Success, "Your password is successfully changed!", 1200);
                    Navigator.NavigateTo<LoginViewModel>();
                    break;

                case ChangePasswordResponseType.SmthWentWrong:
                    Notifier.Notify(MessageBoxType.Error, "Something went wrong. Try again later...");
                    break;
            }
        }
    }
}
