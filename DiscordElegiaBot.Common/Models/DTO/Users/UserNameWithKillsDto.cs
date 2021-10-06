namespace DiscordElegiaBot.Common.Models.DTO.Users
{
    public class UserNameWithKillsDto
    {
        public string UserName { get; set; }
        public int Kills { get; set; }

        public override string ToString()
        {
            return $"{UserName,-25} Убийств - {Kills.ToString()}";
        }
    }
}