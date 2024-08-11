using Chat.MVVM.Models.Handlers;
using CommonLibrary;
using ProtocolLibrary.Message;
using SocketEventLibrary.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Services
{
    public class SocketEventConfigurator
    {
        public static void ConfigurateSocketEvent(SocketEvent socket)
        {
            //1. Sets supported SocketMessage's Types for income
            socket.AddSupportedMessageType<SocketEventProtocolMessage>();

            //2. Subscribes on Events from Client
            socket.On(MessageType.RegistrationResponse, 
                (mes) => RegistrationService.HandleResponse((ProtocolMessage)mes));

            socket.On(MessageType.EmailVerificationResponse,
                (mes) => EmailVerifier.HandleResponse((ProtocolMessage)mes));

            socket.On(MessageType.SigningInResponse,
                (mes) => SigningInService.HandleResponse((ProtocolMessage)mes));

            socket.On(MessageType.SessionStateCheckResponse,
                (mes) => SessionStateChecker.HandleResponse((ProtocolMessage)mes));

            socket.On(MessageType.ResetPasswordResponse,
                (mes) => PasswordResetter.HandleResponse((ProtocolMessage)mes));

            socket.On(MessageType.VerifyCodeForResetPasswordResponse,
                (mes) => CodeVerifierForResetPassword.HandleResponse((ProtocolMessage)mes));

            socket.On(MessageType.ChangePasswordResponse, 
                (mes) => PasswordChanger.HandleResponse((ProtocolMessage)mes));

            //3. Subscribes on service Events
            //TODO:
            //
            //
        }
    }
}
