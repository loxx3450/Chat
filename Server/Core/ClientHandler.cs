﻿using CommonLibrary;
using ProtocolLibrary.Message;
using ServerSide.Core.Handlers;
using ServerSide.Core.Static;
using SocketEventLibrary.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core
{
    internal class ClientHandler
    {
        public static void HandleClient(SocketEvent socket)
        {
            Console.WriteLine($"Client is connected at {DateTime.Now}");

            //Logic of handling clients after connection
            Client client = new Client(socket);

            //1. Sets supported SocketMessage's Types for income
            socket.AddSupportedMessageType<SocketEventProtocolMessage>();

            //2. Subscribes on Events from Client
            socket.On(MessageType.RegistrationRequest, (message) =>
            {
                RegistrationHandler.TryToCreateUser((ProtocolMessage)message);
                socket.Emit(RegistrationHandler.GetResponse());
            });

            socket.On(MessageType.EmailVerificationRequest, (message) =>
            {
                EmailVerifier.Verify((ProtocolMessage)message);
                socket.Emit(EmailVerifier.GetResponse());
            });

            socket.On(MessageType.SigningInRequest, (message) =>
            {
                SigningInHandler.TryToSignIn((ProtocolMessage)message);
                socket.Emit(SigningInHandler.GetResponse());
            });

            socket.On(MessageType.SessionStateCheckRequest, (message) =>
            {
                SessionStateChecker.Check((ProtocolMessage)message);
                socket.Emit(SessionStateChecker.GetResponse());
            });

            socket.On(MessageType.ResetPasswordRequest, (message) =>
            {
                PasswordResetter.TryToSendEmail((ProtocolMessage)message);
                socket.Emit(PasswordResetter.GetResponse());
            });

            socket.On(MessageType.VerifyCodeForResetPasswordRequest, (message) =>
            {
                CodeVerifierForResetPassword.VerifyCode((ProtocolMessage)message);
                socket.Emit(CodeVerifierForResetPassword.GetResponse());
            });

            socket.On(MessageType.ChangePasswordRequest, (message) =>
            {
                PasswordChanger.ChangePassword((ProtocolMessage)message);
                socket.Emit(PasswordChanger.GetResponse());
            });

            socket.On(MessageType.GettingDialoguesCardsRequest, (message) =>
            {
                DialoguesCardsProvider.FormulateListOfDialogues((ProtocolMessage)message);
                socket.Emit(DialoguesCardsProvider.GetResponse());
            });

            socket.On(MessageType.MessagesUploadRequest, (message) =>
            {
                MessagesProvider.FormulateListOfMessages((ProtocolMessage)message);
                socket.Emit(MessagesProvider.GetResponse());
            });

            //3. Subscribes on service Events
            socket.OnDisconnecting += () => DisconnectionHandler.Disconnect(client);
            socket.OnOtherSideIsDisconnected += () => BreakUpHandler.HandleBreakUp(client);
        }
    }
}
