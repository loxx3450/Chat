using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    internal class ClientsCollection
    {
        private List<Client> clients;

        public void AddClient(Client client)
        {
            clients.Add(client);
        }

        //Methods to get Clients in different ways
        //...
    }
}
