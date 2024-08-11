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
using Chat.MVVM.Models.Services;
using Chat.MVVM.ViewModels.EntryWindows;

namespace Chat.MVVM.Models.Handlers
{
    internal class CodeVerifierForResetPassword : IHandler
    {
        //Verify Recovery code
        public static void VerifyCode(string code)
        {
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new VerifyCodeForResetPasswordRequestPayload(code, Client.AssociatedUserId));

            SocketEventHandler.EmitAndWait(new SocketEventProtocolMessage(MessageType.VerifyCodeForResetPasswordRequest, message));
        }

        public static void HandleResponse(ProtocolMessage message)
        {
            TransitionManager.RemoveWaiting();

            var payload = PayloadBuilder.GetPayload<VerifyCodeForResetPasswordResponsePayload>(message.PayloadStream);

            switch (payload.ResponseType)
            {
                case VerifyCodeForResetPasswordResponseType.Success:
                    Navigator.NavigateTo<ChangePasswordViewModel>();
                    break;

                case VerifyCodeForResetPasswordResponseType.Failed:
                    Notifier.Notify(MessageBoxType.Error, "The code is either wrong, or expired. Try again.");
                    break;
            }
        }
    }
}
