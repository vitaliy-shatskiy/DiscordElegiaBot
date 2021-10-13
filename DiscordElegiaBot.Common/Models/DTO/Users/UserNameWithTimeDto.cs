using System;

namespace DiscordElegiaBot.Common.Models.DTO.Users
{
    public class UserNameWithTimeDto
    {
        public string UserName { get; set; }
        public int Playtime { get; set; }

        public override string ToString()
        {
            return $"{UserName,-25} Время - {TimeSpan.FromSeconds(Playtime).TotalHours:N2} Часов";
        }
    }
}