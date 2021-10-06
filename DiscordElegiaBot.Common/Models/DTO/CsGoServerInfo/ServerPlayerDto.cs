using Newtonsoft.Json;

namespace DiscordElegiaBot.Common.Models.DTO.CsGoServerInfo
{
    public class ServerPlayerDto
    {
        [JsonProperty("Name")] public string NickName { get; set; }

        [JsonProperty("Frags")] public int Frags { get; set; }

        [JsonProperty("TimeF")] public string PlayingTime { get; set; }
    }
}