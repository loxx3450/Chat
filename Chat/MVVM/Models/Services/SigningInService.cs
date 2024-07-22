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

namespace Chat.MVVM.Models.Services
{
    public class SigningInService
    {
        public static void SignIn(string email, string password)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new SigningInRequestPayload(email, password));

            SocketEventHandler.Emit(new SocketEventProtocolMessage(MessageType.SigningInRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message) 
        {
            SigningInResponsePayload payload = PayloadBuilder.GetPayload<SigningInResponsePayload>(message.PayloadStream);

            switch (payload.ResponseType)
            {
                case SigningInResponseType.Successed:
                    Notifier.Notify(MessageBoxType.Success, "You are signed in!");
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
