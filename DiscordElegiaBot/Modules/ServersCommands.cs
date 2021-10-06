using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordElegiaBot.BLL.Providers;
using DiscordElegiaBot.BLL.Services.Abstract;
using DiscordElegiaBot.Common.Models.Configurations;
using DiscordElegiaBot.Common.Models.DTO.CsGoServerInfo;
using Microsoft.Extensions.Configuration;

namespace DiscordElegiaBot.Modules
{
    public class ServersCommands : ModuleBase
    {
        private readonly Config _config;
        private readonly ServerInfoProvider _serverInfoProvider;

        public ServersCommands(ServerInfoProvider serverInfoProvider, IConfiguration configuration,
            IUserService userService)
        {
            _serverInfoProvider = serverInfoProvider;
            _config = new Config();
            configuration.Bind(_config);
        }

        [Command("null")]
        public async Task CommandZ()
        {
            var sb = new StringBuilder();
            var user = await Context.Client.GetUserAsync(245938852668112897);
            sb.AppendLine(
                $"Бот сделан из говна и палок под пиво \"*Жигулевское*\" вот этим дебилом -> [{user.Mention}]");
            await ReplyAsync(sb.ToString());
        }

        [Command("ip")]
        [Alias("айпи", "ип", "сервера", "servers")]
        public async Task ServersIp()
        {
            await ReplyAsync(
                $"```css\nСервера Elegia:\nMirage: {_config.Mirage.Ip}\nPublic: {_config.Public.Ip}\n```");
        }

        [Command("site")]
        [Alias("сайт")]
        public async Task ServerSite()
        {
            await ReplyAsync($"Сайт сервера: {_config.ServerSiteUrl}");
        }

        [Command("public")]
        [Alias("2", "pub")]
        public async Task ServerPublicInfo()
        {
            try
            {
                var serverInfo = await _serverInfoProvider.GetServerInfoAsync(_config.Public.Ip);
                if (serverInfo.PlayersCount == 0)
                {
                    await ReplyAsync($"На Public {_config.Public.Ip} нет игроков :(");
                    return;
                }

                serverInfo.PlayersList.RemoveAt(0);
                await ReplyAsync(null, false, GetServerMessageBodyEmbedBuilder(serverInfo, Color.DarkPurple));
            }
            catch
            {
                await ReplyAsync($"Public {_config.Public.Ip} не отвечает...");
            }
        }

        [Command("mirage")]
        [Alias("1", "мираж")]
        public async Task ServerMirageInfo()
        {
            try
            {
                var serverInfo = await _serverInfoProvider.GetServerInfoAsync(_config.Mirage.Ip);
                if (serverInfo.PlayersCount == 0)
                {
                    await ReplyAsync($"На Mirage {_config.Mirage.Ip} нет игроков :(");
                    return;
                }

                serverInfo.PlayersList.RemoveAt(0);
                await ReplyAsync(null, false, GetServerMessageBodyEmbedBuilder(serverInfo, Color.DarkOrange));
            }
            catch
            {
                await ReplyAsync($"Mirage {_config.Mirage.Ip} не отвечает...");
            }
        }

        [Command("demos")]
        [Alias("dem", "дем", "демо", "демка", "demo", "демки")]
        public async Task GetMirageDemosInfo()
        {
            try
            {
                var demos = await _serverInfoProvider.GetMirageDemos();
                var embed = new EmbedBuilder
                {
                    Title = "Демки Mirage Only",
                    Color = Color.Red
                };
                foreach (var demo in demos)
                    embed.AddField($"Дата       {demo.Modified.ToLocalTime().ToString(CultureInfo.InvariantCulture)}",
                        $"[Скачать](http://{_config.Mirage.FtpHost}/{_config.Mirage.FtpUser}/{demo.Name}) {(demo.Size / 1024 / 1024).ToString()} MB");

                await ReplyAsync(null, false, embed.Build());
            }
            catch
            {
                await ReplyAsync("Не удалось загрузить демки");
            }
        }

        private static Embed GetServerMessageBodyEmbedBuilder(ServerInfoDto serverInfo, Color leftLineColor)
        {
            var sb = new StringBuilder();
            sb.Append("```");
            sb.Append("╔════════╤═════╤═══════════════════════════╗\n");
            sb.Append($"║{"Время",-8}│{"Очки",-5}│{"Игрок",-26} ║\n");
            sb.Append("╠════════╪═════╪═══════════════════════════╝\n");
            for (var i = 0; i < serverInfo.PlayersCount; i++)
            {
                var player = serverInfo.PlayersList[i];
                sb.Append($"║{player.PlayingTime,-8}│{player.Frags.ToString(),-5}│{player.NickName}\n");
                if (i < serverInfo.PlayersCount - 1)
                    sb.Append("╟────────┼─────┼───────────────────────────╢\n");
            }

            sb.Append("╚════════╧═════╧═══════════════════════════╝");
            sb.Append("```");
            return new EmbedBuilder
            {
                Title =
                    $"{serverInfo.HostName.Remove(serverInfo.HostName.Length - 2)}\n{serverInfo.Ip,-20}{$"Игроков: {serverInfo.PlayersCount.ToString()}/{serverInfo.MaxPlayers.ToString()}",44}",
                Color = leftLineColor,
                Description = sb.ToString()
            }.Build();
        }
    }
}