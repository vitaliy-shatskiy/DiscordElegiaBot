namespace DiscordElegiaBot.Common.Models.DTO.Users
{
    public class UserNameWithDeathsDto
    {
        public string UserName { get; set; }
        public int Deaths { get; set; }

        public override string ToString()
        {
            return $"{UserName,-25} Смертей - {Deaths.ToString()}";
        }
    }
}