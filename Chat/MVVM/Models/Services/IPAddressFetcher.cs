using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Services
{
    public static class IPAddressFetcher
    {
        public static IPAddress GetIPAddress()
        {
            foreach (IPAddress address in Dns.GetHostAddresses(Environment.MachineName))
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    return address;
                }
            }

            //TODO
            throw new NotImplementedException();
        }
    }
}
