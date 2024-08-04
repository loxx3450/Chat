using Chat.MVVM.Models.Instances;
using Chat.MVVM.Models.Services;
using Chat.MVVM.ViewModels;
using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using CommonLibrary;
using CommonLibrary.Payloads.Registration;
using CommonLibrary.Payloads.ResetingPassword;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Handlers
{
    public class PasswordResetter : IHandler
    {
        public static void ResetPassword(string email)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new ResetPasswordRequestPayload(email));

            SocketEventHandler.EmitAndWait(new SocketEventProtocolMessage(MessageType.ResetPasswordRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            TransitionManager.RemoveWaiting();

            var payload = PayloadBuilder.GetPayload<ResetPasswordResponsePayload>(message.PayloadStream);

            switch (payload.ResponseType)
            {
                case ResetPasswordResponseType.Success:
                    Client.AssociatedUserId = payload.AssociatedUserId;
                    Navigator.NavigateTo<CodeConfirmationViewModel>();
                    break;

                case ResetPasswordResponseType.Failed:
                    Notifier.Notify(MessageBoxType.Error, "User with such email does not exist. Try to create a new account.");
                    break;

                case ResetPasswordResponseType.SmthWentWrong:
                    Notifier.Notify(MessageBoxType.Error, "Something went wrong. Try again later...");
                    break;
            }
        }
    }
}
