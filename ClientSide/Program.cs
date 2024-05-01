using ClientSide.Core;
using CommonLibrary;
using CommonLibrary.Payloads;
using ProtocolLibrary.Message;
using SocketEventLibrary.SocketEventMessageCore;
using SocketEventLibrary.Sockets;

const string hostname = "127.0.0.1";
const int port = 80;

ClientSocketEvent clientSocket = new ClientSocketEvent(hostname, port);

SocketEvent socket = await clientSocket.GetSocketAsync();

SocketEventHandler.HandleSocket(socket);

bool isConnected = true;

while (isConnected)
{
    //Test Code for calling differen operations
    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.Escape:
            socket.Disconnect();

            isConnected = false;

            break;

        case ConsoleKey.Enter:
            ProtocolMessage message = new ProtocolMessage();
            message.SetPayload(new RegistrationRequestPayload(new CommonLibrary.Models.User("new_user", "new_password", "new_fullName", new DateOnly(2006, 1, 15), true)));

            socket.Emit(new SocketEventProtocolMessage(MessageType.RegistrationRequest, message));

            break;
    }
}