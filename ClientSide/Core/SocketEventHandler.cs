using ClientSide.Core.Handlers;
using CommonLibrary;
using CommonLibrary.Payloads.Registration;
using CommonLibrary.Payloads.SigningIn;
using ProtocolLibrary.Core;
using ProtocolLibrary.Message;
using SocketEventLibrary.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Core
{
    internal class SocketEventHandler
    {
        public static void HandleSocket(SocketEvent socket)
        {
            //1. Sets supported SocketMessage's Types for income
            socket.AddSupportedMessageType<SocketEventProtocolMessage>();

            //2. Subscribes on Events from Client
            socket.On(MessageType.RegistrationResponse, (mes) =>
            {
                //Test Code
                RegistrationResponsePayload payload = PayloadBuilder.GetPayload<RegistrationResponsePayload>(((ProtocolMessage)mes).PayloadStream);
                switch(payload.ResponseType)
                {
                    case RegistrationResponseType.Successed:
                        Console.WriteLine("Registration completed successfully!");
                        break;
                    case RegistrationResponseType.Failed:
                        Console.WriteLine("Some problem is occured: Check your data!");
                        break;
                    case RegistrationResponseType.UserAlreadyExists:
                        Console.WriteLine("User with such data already exists!");
                        break;
                }
            });

            socket.On(MessageType.SigningInResponse, (mes) =>
            {
                //TestCoed
                SigningInResponsePayload payload = PayloadBuilder.GetPayload<SigningInResponsePayload>(((ProtocolMessage)mes).PayloadStream);
                switch(payload.ResponseType) 
                { 
                    case SigningInResponseType.Successed:
                        Console.WriteLine("You are signed in");
                        break;
                    case SigningInResponseType.Failed:
                        Console.WriteLine("Your data is wrong");
                        break;
                    case SigningInResponseType.SmthWentWrong:
                        Console.WriteLine("Sonmething went wrong during signing in");
                        break;
                }
            });

            //3. Subscribes on service Events
            socket.OnThrowedException += ExceptionHandler.HandleException;
            socket.OnDisconnecting += DisconnectionHandler.Disconnect;
            socket.OnOtherSideIsDisconnected += BreakUpHandler.HandleBreakUp;
        }
    }
}
