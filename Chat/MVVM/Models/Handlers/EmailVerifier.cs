using Chat.MVVM.Models.Instances;
using Chat.MVVM.Models.Services;
using Chat.MVVM.ViewModels;
using Chat.MVVM.ViewModels.EntryWindows;
using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using CommonLibrary;
using CommonLibrary.Payloads.Registration;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Handlers
{
    internal class EmailVerifier : IHandler
    {
        public static void Verify(string code)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new EmailVerificationRequestPayload(code, Client.AssociatedUserId));

            SocketEventHandler.EmitAndWait(new SocketEventProtocolMessage(MessageType.EmailVerificationRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            TransitionManager.RemoveWaiting();

            EmailVerificationResponsePayload payload = PayloadBuilder.GetPayload<EmailVerificationResponsePayload>(message.PayloadStream);
            
            switch (payload.ResponseType)
            {
                case EmailVerificationResponseType.Success:
                    Notifier.Notify(MessageBoxType.Success, "You completed basic registration and now can use almost every feature! :-)");
                    Navigator.NavigateTo<LoginViewModel>();
                    break;

                case EmailVerificationResponseType.Failed:
                    Notifier.Notify(MessageBoxType.Error, "The code is either wrong, or expired. Try again.");
                    break;

                case EmailVerificationResponseType.SmthWentWrong:
                    Notifier.Notify(MessageBoxType.Error, "Something went wrong. Try again later...");
                    break;
            }
        }
    }
}
