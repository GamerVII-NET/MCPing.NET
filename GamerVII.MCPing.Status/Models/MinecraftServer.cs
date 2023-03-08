namespace GamerVII.MCPing.Status.Models
{
    public class MinecraftServer
    {
        public string? HostAddress { get; private set; }
        public string? Port { get; private set; }
        public string[]? Players { get; private set; }
        public string? Version { get; private set; }
        public string? Name { get; private set; }
        public int MaxPlayersCount { get; private set; }
        public int PlayersCount { get; private set; }
        public string? Map { get; private set; }

        internal static MinecraftServer Parse(MinecraftServerInfo minecraftServerInfo)
        {
            string[] players = new string[minecraftServerInfo.Players.Count];

            int.TryParse(minecraftServerInfo.Parameters["numplayers"], out int playersCount);
            int.TryParse(minecraftServerInfo.Parameters["numplayers"], out int maxPlayersCount);

            return new MinecraftServer
            {
                HostAddress = minecraftServerInfo.Parameters["hostip"],
                Port = minecraftServerInfo.Parameters["hostport"],
                Players = minecraftServerInfo.Players.ToArray(),
                Version = minecraftServerInfo.Parameters["version"],
                Name = minecraftServerInfo.Parameters["hostname"],
                Map = minecraftServerInfo.Parameters["map"],
                PlayersCount = playersCount,
                MaxPlayersCount = maxPlayersCount,
            };

        }
    }
}
