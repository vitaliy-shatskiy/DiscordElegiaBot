using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordElegiaBot.Services
{
    public static class СommandsExecutors
    {
        private const string CsGoMirageIp = "91.211.118.36:27045";
        private const string CsGoAwpIp = "45.136.204.89:27015";

        public static async Task GoodbyeExecutor(SocketMessage message)
        {
            await message.Channel.SendMessageAsync($"Сладких снов :kissing_heart: {message.Author.Mention}");
        }

        public static async Task RandomImageExecutor(SocketMessage message, string command)
        {
            try
            {
                var image = command.Contains(' ')
                    ? await RandomImageHttpRequest.GetRandomPhoto(command.Split(' ')[1])
                    : await RandomImageHttpRequest.GetRandomPhoto();
                await message.Channel.SendFileAsync(image.Stream, "randomPhoto.jpg");
            }
            catch
            {
                await message.Channel.SendMessageAsync("Не могу загрузить случайную картинку :confused:");
            }
        }

        public static async Task AccountAgeExecutor(SocketMessage message, string command, DiscordSocketClient client)
        {
            try
            {
                if (command.Contains("<@!"))
                {
                    var commandWithTag = command.Split(" ");
                    var targetUser =
                        client.GetUser(Convert.ToUInt64(commandWithTag[1]
                            .Remove(commandWithTag[1].Length - 1, 1)
                            .Remove(0, 3)));
                    await message.Channel.SendMessageAsync(
                        $"Аккаунт {targetUser.Mention} создан: {targetUser.CreatedAt.DateTime}");
                }
                else
                {
                    var author = message.Author;
                    await message.Channel.SendMessageAsync(
                        $"Аккаунт {author.Mention} создан: {author.CreatedAt.DateTime}");
                }
            }
            catch
            {
                await message.Channel.SendMessageAsync("Не могу вывести дату :confused:");
            }
        }

        public static async Task CoinFlipExecutor(SocketMessage message)
        {
            Random random = new();
            await message.Channel.SendMessageAsync(random.NextDouble() < 0.5 ? @"Решка" : @"Орёл");
        }

        public static async Task ServersIpExecutor(SocketMessage message)
        {
            await message.Channel.SendMessageAsync(
                $"```css\nСервера:\nMirage Only: {CsGoMirageIp}\nAWP Only: {CsGoAwpIp}\n```");
        }

        public static async Task ServerAwpIpExecutor(SocketMessage message)
        {
            try
            {
                var serverInfo = await ServerInfoHttpRequest.GetServerInfo(CsGoAwpIp);
                await message.Channel
                    .SendMessageAsync(
                        serverInfo
                            .PlayersList
                            .Aggregate(
                                $"```|{serverInfo.HostName,-53}|" +
                                $"\n|{serverInfo.Ip,-20}{$"Игроков: {serverInfo.PlayersCount}/{serverInfo.MaxPlayers}",33}|" +
                                "\n_______________________________________________________" +
                                $"\n|{"Игрок",-30}|{"Фраги",-5}|{"Время на сервере",-16}|\n",
                                (str, player) =>
                                    str +
                                    $"|{player.NickName,-30}|{player.Frags,-5}|{player.PlayingTime,-16}|\n")
                        + "```");
            }
            catch
            {
                await message.Channel.SendMessageAsync($"AWP {CsGoMirageIp} не отвечает.");
            }
        }

        public static async Task ServerMirageIpExecutor(SocketMessage message)
        {
            try
            {
                var serverInfo = await ServerInfoHttpRequest.GetServerInfo(CsGoMirageIp);
                await message.Channel
                    .SendMessageAsync(
                        serverInfo
                            .PlayersList
                            .Aggregate(
                                $"```|{serverInfo.HostName.Remove(serverInfo.HostName.Length - 2, 2),-53}|" +
                                $"\n|{serverInfo.Ip,-20}{$"Игроков: {serverInfo.PlayersCount}/{serverInfo.MaxPlayers}",33}|" +
                                "\n_______________________________________________________" +
                                $"\n|{"Игрок",-30}|{"Фраги",-5}|{"Время на сервере",-16}|\n",
                                (str, player) =>
                                    str +
                                    $"|{player.NickName,-30}|{player.Frags,-5}|{player.PlayingTime,-16}|\n")
                        + "```");
            }
            catch
            {
                await message.Channel.SendMessageAsync($"Mirage {CsGoMirageIp} не отвечает.");
            }
        }

        public static async Task ServerSiteExecutor(SocketMessage message)
        {
            await message.Channel.SendMessageAsync("Сайт сервера: https://elegia-project.su/");
        }

        public static async Task HelloExecutor(SocketMessage message)
        {
            await message.Channel.SendMessageAsync($"Дароу :grin: {message.Author.Mention}");
        }
    }
}