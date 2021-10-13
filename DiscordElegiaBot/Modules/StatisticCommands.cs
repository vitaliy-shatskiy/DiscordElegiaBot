using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordElegiaBot.BLL.Services.Abstract;

namespace DiscordElegiaBot.Modules
{
    public class StatisticCommands : ModuleBase
    {
        private readonly IUserService _userService;

        public StatisticCommands(IUserService userService)
        {
            _userService = userService;
        }


        [Command("player")]
        public async Task PlayerInfo([Remainder] string userName = " ")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    await ReplyAsync("Напишите через пробел ник или SteamId игрока (-player Vasya)");
                    return;
                }

                var user = await _userService.GetUserInfoAsync(userName);
                await ReplyAsync($"```{user}```");
            }
            catch (DuplicateNameException e)
            {
                var embed = new EmbedBuilder
                {
                    Title = "Найдены дубликаты",
                    Color = Color.DarkRed
                }.AddField($"{userName}", e.Message);

                await ReplyAsync(null, false, embed.Build());
            }
            catch (KeyNotFoundException e)
            {
                await ReplyAsync($"Ошибка: {e.Message}");
            }
            catch
            {
                await ReplyAsync("Не могу загрузить инфу :confused:");
            }
        }


        [Command("top score")]
        public async Task TopScore()
        {
            try
            {
                var users = (await _userService.GetTopUsersByScoreAsync()).ToList();
                var sb = new StringBuilder();
                for (var i = 0; i < users.Count; i++) sb.AppendLine($"{(i + 1).ToString(),+2}. {users[i]}");

                await ReplyAsync($"```{sb}```");
            }
            catch
            {
                await ReplyAsync("Не могу загрузить инфу :confused:");
            }
        }

        [Command("top kills")]
        public async Task TopKills()
        {
            try
            {
                var users = (await _userService.GetTopUsersByKillsAsync()).ToList();
                var sb = new StringBuilder();
                for (var i = 0; i < users.Count; i++) sb.AppendLine($"{(i + 1).ToString(),+2}. {users[i]}");

                await ReplyAsync($"```{sb}```");
            }
            catch
            {
                await ReplyAsync("Не могу загрузить инфу :confused:");
            }
        }

        [Command("top deaths")]
        public async Task TopDeaths()
        {
            try
            {
                var users = (await _userService.GetTopUsersByDeathsAsync()).ToList();
                var sb = new StringBuilder();
                for (var i = 0; i < users.Count; i++) sb.AppendLine($"{(i + 1).ToString(),+2}. {users[i]}");

                await ReplyAsync($"```{sb}```");
            }
            catch
            {
                await ReplyAsync("Не могу загрузить инфу :confused:");
            }
        }

        [Command("top time")]
        public async Task TopTime()
        {
            try
            {
                var users = (await _userService.GetTopUsersByTimeAsync()).ToList();
                var sb = new StringBuilder();
                for (var i = 0; i < users.Count; i++) sb.AppendLine($"{(i + 1).ToString(),+2}. {users[i]}");

                await ReplyAsync($"```{sb}```");
            }
            catch
            {
                await ReplyAsync("Не могу загрузить инфу :confused:");
            }
        }
    }
}