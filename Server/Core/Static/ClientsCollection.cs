using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Core.Static
{
    internal static class ClientsCollection
    {
        private static List<Client> clients = new List<Client>();

        public static void AddClient(Client client)
        {
            clients.Add(client);
        }

        public static Client GetClient(Func<Client, bool> predicate)
        {
            //maybe validation

            return clients.First(predicate);
        }

        //Methods to get Clients in different ways
        //...
    }
}
