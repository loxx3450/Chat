using CommonLibrary.Payloads.ResetingPassword;
using CommonLibrary;
using ProtocolLibrary.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.MVVM.Models.Instances;
using ProtocolLibrary.Core;
using Chat.MVVM.Views.UserControls.AdditionalInfrastructure;
using Chat.MVVM.ViewModels;

namespace Chat.MVVM.Models.Services
{
    internal class CodeVerifier
    {
        //Verify Recovery code
        public static void VerifyCode(string code)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new VerifyCodeRequestPayload(code, Client.AssociatedUserId));

            SocketEventHandler.Emit(new SocketEventProtocolMessage(MessageType.VerifyCodeRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            VerifyCodeResponsePayload payload = PayloadBuilder.GetPayload<VerifyCodeResponsePayload>(message.PayloadStream);

            switch (payload.ResponseType)
            {
                case VerifyCodeResponseType.Success:
                    Navigator.NavigateTo<ChangePasswordViewModel>();
                    break;

                case VerifyCodeResponseType.Failed:
                    Notifier.Notify(MessageBoxType.Error, "The code is either wrong, or expired. Try again.");
                    break;
            }
        }
    }
}
