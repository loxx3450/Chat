using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    internal static class ClientsCollection
    {
        private static List<Client> clients;

        public static void AddClient(Client client)
        {
            clients.Add(client);
        }

        //Methods to get Clients in different ways
        //...
    }
}
