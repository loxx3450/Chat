using ClientSide.Core;
using SocketEventLibrary.Sockets;

const string hostname = "127.0.0.1";
const int port = 80;

ClientSocketEvent clientSocket = new ClientSocketEvent(hostname, port);

SocketEvent socket = await clientSocket.GetSocketAsync();

SocketEventHandler.HandleSocket(socket);

while (true)
{
    if (Console.ReadKey().Key == ConsoleKey.Escape)
    {
        socket.Disconnect();

        break;
    }
}