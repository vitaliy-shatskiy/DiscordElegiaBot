#nullable disable

namespace DiscordElegiaBot.DAL.Entities
{
    public class UserHit
    {
        public string SteamId { get; set; }
        public int DmgHealth { get; set; }
        public int DmgArmor { get; set; }
        public int Head { get; set; }
        public int Chest { get; set; }
        public int Belly { get; set; }
        public int LeftArm { get; set; }
        public int RightArm { get; set; }
        public int LeftLeg { get; set; }
        public int RightLeg { get; set; }
        public int Neak { get; set; }

        public override string ToString()
        {
            return $"Урона по здоровью - {DmgHealth.ToString()}\n" +
                   $"Урона по броне - {DmgArmor.ToString()}\n" +
                   $"Убийства в голову - {Head.ToString()}\n" +
                   $"Убийства в грудь - {Chest.ToString()}\n" +
                   $"Убийства в живот - {Belly.ToString()}\n" +
                   $"Убийства в л.руку - {LeftArm.ToString()}\n" +
                   $"Убийства в п.руку - {RightArm.ToString()}\n" +
                   $"Убийства в л.ногу - {LeftLeg.ToString()}\n" +
                   $"Убийства в п.ногу - {RightLeg.ToString()}\n" +
                   $"Убийства в шею - {Neak.ToString()}";
        }
    }
}