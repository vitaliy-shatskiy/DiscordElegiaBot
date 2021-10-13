namespace DiscordElegiaBot.Common.Models.DTO.Users
{
    public class UserNameWithScoreDto
    {
        public string UserName { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return $"{UserName,-25} Очки - {Score.ToString()}";
        }
    }
}