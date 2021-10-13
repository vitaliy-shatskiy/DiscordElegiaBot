#nullable disable

namespace DiscordElegiaBot.DAL.Entities
{
    public class UserUnusualKill
    {
        public string SteamId { get; set; }
        public int Op { get; set; }
        public int Penetrated { get; set; }
        public int NoScope { get; set; }
        public int Run { get; set; }
        public int Jump { get; set; }
        public int Flash { get; set; }
        public int Smoke { get; set; }
        public int Whirl { get; set; }
        public int LastClip { get; set; }

        public override string ToString()
        {
            return $"Килл прострелом - {Penetrated.ToString()}\n" +
                   $"Килл NoScope - {NoScope.ToString()}\n" +
                   $"Килл в беге - {Run.ToString()}\n" +
                   $"Килл в крыжке - {Jump.ToString()}\n" +
                   $"Килл флешкой - {Flash.ToString()}\n" +
                   $"Килл смоком - {Smoke.ToString()}\n" +
                   $"Килл разворотом - {Whirl.ToString()}\n" +
                   $"Последним выстрелом - {LastClip.ToString()}";
        }
    }
}