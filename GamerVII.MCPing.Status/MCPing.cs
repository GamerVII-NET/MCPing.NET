using System.Net;
using System.Net.Sockets;
using GamerVII.MCPing.Status.Models;
using GamerVII.MCPing.Status.Helpers;

namespace GamerVII.MCPing.Status
{
    public class MCPing
    {
        public int Port { get; set; }
        public IPAddress Address { get; set; }

        public MCPing(string hostAddress, int port = 25565)
        {
            Address = HostNameParser.GetAddressFromHostName(hostAddress);
            Port = port;
        }

        public MinecraftServer GetStatus()
        {
            var endPoint = new IPEndPoint(IPAddress.Any, Port);

            using (var udpClient = new UdpClient(endPoint))
            {
                try
                {
                    var udpState = new UdpState
                    {
                        EndPoint = endPoint,
                        Client = udpClient
                    };

                    udpClient.Connect(Address, Port);

                    var serverInfo = MinecraftPingHelper.GetStatus(udpState);

                    MinecraftServerInfo minecraftServerInfo = new MinecraftServerInfo(serverInfo);

                    return MinecraftServer.Parse(minecraftServerInfo);
                }
                finally
                {
                    udpClient.Close();
                }
            }
        }

    }
}