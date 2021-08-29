using System;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordElegiaBot.Models;
using DiscordElegiaBot.Models.DTO.CsGoServerInfo;
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
                $"```css\nСервера Elegia:\nMirage: {Settings.CsGoMirageIp}\nPublic: {Settings.CsGoPublic}\n```");
        }

        [Command("site")]
        [Alias("сайт")]
        public async Task ServerSite()
        {
            await ReplyAsync("Сайт сервера: https://elegia.club/");
        }

        [Command("public")]
        [Alias("2", "pub")]
        public async Task ServerPublicInfo()
        {
            try
            {
                var serverInfo = await ServerInfoHttpProvider.GetServerInfoAsync(Settings.CsGoPublic);
                if (serverInfo.PlayersCount == 0)
                {
                    await ReplyAsync($"На Public {Settings.CsGoPublic} нет игроков :(");
                    return;
                }

                serverInfo.PlayersList.RemoveAt(0);
                await ReplyAsync(null, false, GetServerMessageBodyEmbedBuilder(serverInfo, Color.DarkPurple));
            }
            catch
            {
                await ReplyAsync($"Public {Settings.CsGoPublic} не отвечает...");
            }
        }

        [Command("mirage")]
        [Alias("1", "мираж")]
        public async Task ServerMirageInfo()
        {
            try
            {
                var serverInfo = await ServerInfoHttpProvider.GetServerInfoAsync(Settings.CsGoMirageIp);
                if (serverInfo.PlayersCount == 0)
                {
                    await ReplyAsync($"На Mirage {Settings.CsGoMirageIp} нет игроков :(");
                    return;
                }

                serverInfo.PlayersList.RemoveAt(0);
                await ReplyAsync(null, false, GetServerMessageBodyEmbedBuilder(serverInfo, Color.DarkOrange));
            }
            catch
            {
                await ReplyAsync($"Mirage {Settings.CsGoMirageIp} не отвечает...");
            }
        }

        private Embed GetServerMessageBodyEmbedBuilder(ServerInfoDTO serverInfo, Color leftLineColor)
        {
            var sb = new StringBuilder();
            sb.Append("```");
            sb.Append("╔════════╤═════╤═══════════════════════════╗\n");
            sb.Append($"║{"Время",-8}│{"Очки",-5}│{"Игрок",-26} ║\n");
            sb.Append("╠════════╪═════╪═══════════════════════════╝\n");
            for (var i = 0; i < serverInfo.PlayersCount; i++)
            {
                var player = serverInfo.PlayersList[i];
                sb.Append($"║{player.PlayingTime,-8}│{player.Frags,-5}│{player.NickName}\n");
                if (i < serverInfo.PlayersCount - 1)
                    sb.Append("╟────────┼─────┼───────────────────────────╢\n");
            }

            sb.Append("╚════════╧═════╧═══════════════════════════╝");
            sb.Append("```");
            return new EmbedBuilder
            {
                Title = serverInfo.HostName.Remove(serverInfo.HostName.Length - 2) +
                        $"\n{serverInfo.Ip,-20}{$"Игроков: {serverInfo.PlayersCount}/{serverInfo.MaxPlayers}",44}",
                Color = leftLineColor,
                Description = sb.ToString()
            }.Build();
        }
    }
}