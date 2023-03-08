using System.Text;

namespace GamerVII.MCPing.Status.Models
{
    internal class MinecraftServerInfo
    {
        public Dictionary<string, string> Parameters;
        public List<string> Players;

        internal MinecraftServerInfo(byte[] message)
        {
            Parameters = new Dictionary<string, string>();
            Players = new List<string>();

            var stream = new MemoryStream(message);
            var stringBuilder = new StringBuilder();
            string lastKey = string.Empty;
            int currentByte;
            var buffer = new byte[256];

            stream.Read(buffer, 0, 5);// Read Type + SessionID
            stream.Read(buffer, 0, 11); // Padding: 11 bytes constant

            while ((currentByte = stream.ReadByte()) != -1)
            {
                if (currentByte == 0x00)
                {
                    if (!string.IsNullOrEmpty(lastKey))
                    {
                        Parameters.Add(lastKey, stringBuilder.ToString());
                        lastKey = string.Empty;
                    }
                    else
                    {
                        lastKey = stringBuilder.ToString();
                        if (string.IsNullOrEmpty(lastKey)) break;
                    }
                    stringBuilder.Clear();
                }
                else stringBuilder.Append((char)currentByte);
            }

            while ((currentByte = stream.ReadByte()) != -1)
            {
                if (currentByte == 0x00)
                {
                    var player = stringBuilder.ToString();
                    if (string.IsNullOrEmpty(player)) continue;
                    Players.Add(player);
                    stringBuilder.Clear();
                }
                else stringBuilder.Append((char)currentByte);
            }
        }
    }
}
