using Newtonsoft.Json;

namespace DiscordElegiaBot.Models.DTO.CsGoServerInfo
{
    public class ServerPlayerDTO
    {
        [JsonProperty("Name")] public string NickName { get; set; }

        [JsonProperty("Frags")] public int Frags { get; set; }

        [JsonProperty("TimeF")] public string PlayingTime { get; set; }
    }
}