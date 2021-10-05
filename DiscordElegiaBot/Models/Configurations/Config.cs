namespace DiscordElegiaBot.Models.Configurations
{
    public class Config
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public string ServerSiteUrl { get; set; }
        public ServerConfig Mirage { get; set; }
        public ServerConfig Public { get; set; }
    }
}