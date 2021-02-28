using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordElegiaBot.DTO.CsGoServerInfo
{
    public class ServerInfoDTO
    {
        [JsonProperty("players")] public List<ServerPlayerDTO> PlayersList { get; set; }

        [JsonProperty("ip")] public string Ip { get; set; }

        [JsonProperty("HostName")] public string HostName { get; set; }

        [JsonProperty("Players")] public int PlayersCount { get; set; }

        [JsonProperty("MaxPlayers")] public int MaxPlayers { get; set; }
    }

    public class ServerPlayerDTO
    {
        [JsonProperty("Name")] public string NickName { get; set; }

        [JsonProperty("Frags")] public int Frags { get; set; }

        [JsonProperty("TimeF")] public string PlayingTime { get; set; }
    }
}