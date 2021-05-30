using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordElegiaBot.Models;
using DiscordElegiaBot.Providers;

namespace DiscordElegiaBot.Modules
{
    public class ServersCommands : ModuleBase
    {
        [Command("null")]
        public async Task CommandZ()
        {
            var sb = new StringBuilder();
            var mydakoff = Context.Client.GetUserAsync(245938852668112897).Result.Mention;
            sb.AppendLine($"Бот сделан из говна и палок под пиво \"*Жигулевское*\" вот этим дебилом -> [{mydakoff}]");
            await ReplyAsync(sb.ToString());
        }

        [Command("ip")]
        [Alias("айпи", "ип", "сервера", "servers")]
        public async Task ServersIp()
        {
            await ReplyAsync(
                $"```css\nСервера:\nMirage Only: {Settings.CsGoMirageIp}\nAWP Only: {Settings.CsGoAwpIp}\n```");
        }

        [Command("site")]
        [Alias("сайт")]
        public async Task ServerSite()
        {
            await ReplyAsync("Сайт сервера: https://elegia-project.su/");
        }

        [Command("awp")]
        [Alias("2", "авп", "a", "а")]
        public async Task ServerAwpInfo()
        {
            try
            {
                var serverInfo = await ServerInfoHttpProvider.GetServerInfoAsync(Settings.CsGoAwpIp);
                var sb = new StringBuilder();
                if (serverInfo.PlayersCount != 0)
                {
                    serverInfo.PlayersList.RemoveAt(0);
                    if (string.IsNullOrWhiteSpace(serverInfo.PlayersList[0].NickName))
                        serverInfo.PlayersList.RemoveAt(0);


                    sb.Append("```╔════════╤════╤══════════════════════════╗\n");
                    sb.Append($"║{"Время",-8}│{"Очки",-4}│{"Игрок",-26}║\n");
                    sb.Append("╠════════╪════╪══════════════════════════╝\n");
                    if (serverInfo.PlayersList.Count != 0)
                    {
                        for (var i = 0; i < serverInfo.PlayersList.Count; i++)
                        {
                            var player = serverInfo.PlayersList[i];
                            sb.Append($"║{player.PlayingTime,-8}│{player.Frags,-4}│{player.NickName}\n");
                            if (i < serverInfo.PlayersList.Count - 1)
                                sb.Append("╟────────┼────┼──────────────────────────╢\n");
                        }

                        sb.Append("╚════════╧════╧══════════════════════════╝");
                    }
                }

                sb.Append("```");
                var embed = new EmbedBuilder
                {
                    Title = serverInfo.HostName.Remove(serverInfo.HostName.Length - 2) + " tick" +
                            $"\n{serverInfo.Ip,-20}{$"Игроков: {serverInfo.PlayersCount}/{serverInfo.MaxPlayers}",37}",
                    Color = Color.Default,
                    Description = sb.ToString()
                };
                await ReplyAsync(null, false, embed.Build());
            }
            catch
            {
                await ReplyAsync($"AWP {Settings.CsGoAwpIp} не отвечает.");
            }
        }


        [Command("mirage")]
        [Alias("1", "мираж", "м", "m")]
        public async Task ServerMirageInfo()
        {
            try
            {
                var serverInfo = await ServerInfoHttpProvider.GetServerInfoAsync(Settings.CsGoMirageIp);
                serverInfo.PlayersList.RemoveAt(0);
                if (string.IsNullOrWhiteSpace(serverInfo.PlayersList[0].NickName ?? "x"))
                    serverInfo.PlayersList.RemoveAt(0);
                var sb = new StringBuilder();
                sb.Append("```");
                sb.Append("╔════════╤═════╤═══════════════════════════╗\n");
                sb.Append($"║{"Время",-8}│{"Фраги",-5}│{"Игрок",-26} ║\n");
                sb.Append("╠════════╪═════╪═══════════════════════════╝\n");
                if (serverInfo.PlayersList.Count != 0)
                {
                    for (var i = 0; i < serverInfo.PlayersList.Count; i++)
                    {
                        var player = serverInfo.PlayersList[i];
                        sb.Append($"║{player.PlayingTime,-8}│{player.Frags,-5}│{player.NickName}\n");
                        if (i < serverInfo.PlayersList.Count - 1)
                            sb.Append("╟────────┼─────┼───────────────────────────╢\n");
                    }

                    sb.Append("╚════════╧═════╧═══════════════════════════╝");
                }

                sb.Append("```");
                var embed = new EmbedBuilder
                {
                    Title = serverInfo.HostName.Remove(serverInfo.HostName.Length - 2) +
                            $"\n{serverInfo.Ip,-20}{$"Игроков: {serverInfo.PlayersCount}/{serverInfo.MaxPlayers}",37}",
                    Color = Color.DarkPurple,
                    Description = sb.ToString()
                };
                await ReplyAsync(null, false, embed.Build());
            }
            catch
            {
                await ReplyAsync($"Mirage {Settings.CsGoMirageIp} не отвечает.");
            }
        }
    }
}