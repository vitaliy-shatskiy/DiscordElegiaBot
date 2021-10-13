using System;
using System.Globalization;

#nullable disable

namespace DiscordElegiaBot.DAL.Entities
{
    public class User
    {
        public string Steam { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Rank { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Shoots { get; set; }
        public int Hits { get; set; }
        public int Headshots { get; set; }
        public int Assists { get; set; }
        public int RoundWin { get; set; }
        public int RoundLose { get; set; }
        public int Playtime { get; set; }
        public int LastConnect { get; set; }

        public override string ToString()
        {
            return $"Информация по игроку {Name}\n" +
                   $"SteamID - {Steam}\n" +
                   $"Очки - {Value.ToString()}\n" +
                   $"Ранг - {Rank.ToString()}\n" +
                   $"Убийства - {Kills.ToString()}\n" +
                   $"Смерти - {Deaths.ToString()}\n" +
                   $"Помощи - {Assists.ToString()}\n" +
                   $"Выстрелы - {Shoots.ToString()}\n" +
                   $"Попадания - {Hits.ToString()}\n" +
                   $"Хедшоты - {Headshots.ToString()}\n" +
                   $"Выиграно раундов - {RoundWin.ToString()}\n" +
                   $"Проиграно раундов - {RoundLose.ToString()}\n" +
                   $"Время игры - {TimeSpan.FromSeconds(Playtime).TotalHours:N2} Часов\n" +
                   $"Последний вход - {new DateTime(1970, 1, 1).AddSeconds(LastConnect).ToLocalTime().ToString(CultureInfo.InvariantCulture)}";
        }
    }
}