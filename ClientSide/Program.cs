using ClientSide.Core;
using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Payloads;
using CommonLibrary.Payloads.Registration;
using CommonLibrary.Payloads.SigningIn;
using ProtocolLibrary.Message;
using SocketEventLibrary.SocketEventMessageCore;
using SocketEventLibrary.Sockets;
using System.Net.Http.Headers;

const string hostname = "127.0.0.1";
const int port = 80;

ClientSocketEvent clientSocket = new ClientSocketEvent(hostname, port);

SocketEvent socket = await clientSocket.GetSocketAsync();

SocketEventHandler.HandleSocket(socket);

bool isConnected = true;

while (isConnected)
{
    ProtocolMessage message;

    //Test Code for calling differen operations
    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.Escape:
            socket.Disconnect();

            isConnected = false;

            break;

        case ConsoleKey.Enter:
            message = new ProtocolMessage();
            message.SetPayload(new RegistrationRequestPayload(new User("new_user", "new_password", "new_fullName")));

            socket.Emit(new SocketEventProtocolMessage(MessageType.RegistrationRequest, message));

            break;

        case ConsoleKey.Tab:
            message = new ProtocolMessage();
            message.SetPayload(new SigningInRequestPayload("user", "password"));

            socket.Emit(new SocketEventProtocolMessage(MessageType.SigningInRequest, message));

            break;
    }
}