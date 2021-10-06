#nullable disable

namespace DiscordElegiaBot.DAL.Entities
{
    public class UserWeapon
    {
        public string Steam { get; set; }
        public string Classname { get; set; }
        public int Kills { get; set; }

        public override string ToString()
        {
            return $"Убийства - {Kills.ToString()} -> {Classname[(Classname.IndexOf('_') + 1)..]}";
        }
    }
}