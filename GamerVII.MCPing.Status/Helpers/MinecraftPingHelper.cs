using GamerVII.MCPing.Status.Models;
using System.Text;

namespace GamerVII.MCPing.Status.Helpers
{
    internal class MinecraftPingHelper
    {

        const Byte Statistic = 0x00;
        const Byte Handshake = 0x09;

        public static byte[] GetStatus(UdpState udpState)
        {
            var challengeToken = GetChallengeToken(udpState, new byte[16]);

            WriteData(udpState, Statistic, challengeToken, new byte[] { 0x00, 0x00, 0x00, 0x00 });

            return ReceiveMessages(udpState);
        }

        private static byte[] GetChallengeToken(UdpState udpState, byte[] challangeBytes)
        {
            WriteData(udpState, Handshake);
            var message = ReceiveMessages(udpState);
            Array.Copy(message, 5, challangeBytes, 0, message.Length - 5);
            var challengeInt = int.Parse(Encoding.ASCII.GetString(challangeBytes));
            return BitConverter.GetBytes(challengeInt).Reverse().ToArray();
        }

        private static void WriteData(UdpState udpState, byte cmd, byte[] append = null, byte[] append2 = null)
        {
            var cmdData = new byte[] { 0xFE, 0xFD, cmd, 0x01, 0x02, 0x03, 0x04 };
            var dataLength = cmdData.Length + (append != null ? append.Length : 0) + (append2 != null ? append2.Length : 0);
            var data = new byte[dataLength];
            cmdData.CopyTo(data, 0);
            if (append != null) append.CopyTo(data, cmdData.Length);
            if (append2 != null) append2.CopyTo(data, cmdData.Length + (append != null ? append.Length : 0));
            udpState.Client.Send(data, data.Length);
        }

        private static byte[] ReceiveMessages(UdpState udpState) =>
            udpState.Client.Receive(ref udpState.EndPoint);
    }
}
