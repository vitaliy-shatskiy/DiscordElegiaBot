using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscordElegiaBot.Common.Models.DTO.CsGoServerInfo
{
    public class ServerInfoDto
    {
        [JsonProperty("players")] public List<ServerPlayerDto> PlayersList { get; set; }

        [JsonProperty("ip")] public string Ip { get; set; }

        [JsonProperty("HostName")] public string HostName { get; set; }

        [JsonProperty("Players")] public int PlayersCount { get; set; }

        [JsonProperty("MaxPlayers")] public int MaxPlayers { get; set; }
    }
}