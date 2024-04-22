using ServerSide;

Server server = new Server();

server.Start();

while (true)
{
    if (Console.ReadKey().Key == ConsoleKey.Escape)
    {
        server.Stop();

        break;
    }
}