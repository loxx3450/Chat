using Chat.MVVM.Models.Instances;
using Chat.MVVM.Models.Services;
using Chat.MVVM.ViewModels;
using Chat.MVVM.ViewModels.EntryWindows;
using Chat.MVVM.Views.UserControls;
using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using CommonLibrary;
using CommonLibrary.Models.EF;
using CommonLibrary.Payloads.Registration.Registration;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using ProtocolLibrary.Payload;
using SocketEventLibrary.SocketEventMessageCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chat.MVVM.Models.Handlers
{
    public class RegistrationService : IHandler
    {
        public static void Register(User user)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new RegistrationRequestPayload(user));

            SocketEventHandler.EmitAndWait(new SocketEventProtocolMessage(MessageType.RegistrationRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            TransitionManager.RemoveWaiting();

            var responsePayload = PayloadBuilder.GetPayload<RegistrationResponsePayload>(message.PayloadStream);

            switch (responsePayload.ResponseType)
            {
                case RegistrationResponseType.Successed:
                    Client.AssociatedUserId = responsePayload.AssociatedUserId;
                    Navigator.NavigateTo<EmailVerificationViewModel>();
                    break;

                case RegistrationResponseType.Failed:
                    Notifier.Notify(MessageBoxType.Error, "Something went wrong. Check that your data is valid and try again.");
                    break;

                case RegistrationResponseType.UserAlreadyExists:
                    Notifier.Notify(MessageBoxType.Error, "User with such email already exists.");
                    break;

                case RegistrationResponseType.SmthWentWrong:
                    Notifier.Notify(MessageBoxType.Error, "Something went wrong. Try again later...");
                    break;
            }
        }
    }
}
