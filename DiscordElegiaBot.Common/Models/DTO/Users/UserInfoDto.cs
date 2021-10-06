using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscordElegiaBot.DAL.Entities;

namespace DiscordElegiaBot.Common.Models.DTO.Users
{
    public class UserInfoDto
    {
        public UserInfoDto()
        {
            UserWeapons = new List<UserWeapon>();
        }

        public int Top { get; set; }
        public User User { get; set; }
        public UserHit UserHit { get; set; }
        public UserUnusualKill UserUnusualKill { get; set; }
        public ICollection<UserWeapon> UserWeapons { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(User.ToString());
            sb.AppendLine($"Топ {Top.ToString()} игрок сервера");
            sb.AppendLine();
            sb.AppendLine(UserHit.ToString());
            sb.AppendLine();
            sb.AppendLine(UserUnusualKill.ToString());
            sb.AppendLine();
            UserWeapons.ToList().ForEach(weapon => sb.AppendLine(weapon.ToString()));
            return sb.ToString();
        }
    }
}