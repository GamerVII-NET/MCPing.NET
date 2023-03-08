using System.Net;

namespace GamerVII.MCPing.Status.Helpers
{
    internal class HostNameParser
    {

        public static IPAddress GetAddressFromHostName(string hostName)
        {
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);

            if (addresses.Length < 1)
            {
                throw new ArgumentException(nameof(hostName));
            }

            return addresses[0];
        }
    }
}
