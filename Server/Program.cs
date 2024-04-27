using ServerSide;

Server server = new Server();

server.Start();


//Test code to manage status of server
bool listenerIsOn = true;
bool serverIsOn = true;

while (serverIsOn)
{
    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.F:
            if (listenerIsOn)
                server.StopAcceptingClients();
            else
                server.StartAcceptingClients();

            listenerIsOn = !listenerIsOn;

            break;

        case ConsoleKey.Escape:
            server.Stop();

            serverIsOn = false;

            break;

    }
}