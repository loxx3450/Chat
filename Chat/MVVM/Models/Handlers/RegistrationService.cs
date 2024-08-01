using Chat.MVVM.Models.Services;
using Chat.MVVM.Views.UserControls;
using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Payloads.Registration;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using SocketEventLibrary.SocketEventMessageCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chat.MVVM.Models.Handlers
{
    public class RegistrationService
    {
        public static void Register(User user)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new RegistrationRequestPayload(user));

            SocketEventHandler.Emit(new SocketEventProtocolMessage(MessageType.RegistrationRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            RegistrationResponsePayload responsePayload = PayloadBuilder.GetPayload<RegistrationResponsePayload>(message.PayloadStream);

            switch (responsePayload.ResponseType)
            {
                case RegistrationResponseType.Successed:
                    Notifier.Notify(MessageBoxType.Success, "Registration went succesfully!", 2000);
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
