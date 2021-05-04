using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using static DiscordElegiaBot.Materials.CommandsStrings;
using static DiscordElegiaBot.Services.СommandsExecutors;

namespace DiscordElegiaBot
{
    public class Program
    {
        private DiscordSocketClient _client;

        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;
            _client.Log += Log;
            var token = await File.ReadAllTextAsync("token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private static Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        private async Task CommandHandler(SocketMessage message)
        {
            if (!message.Content.StartsWith('-'))
                return;
            if (message.Author.IsBot)
                return;
            var command = message.Content.Remove(0, 1).ToLower();
            switch (command)
            {
                case var _ when HelloCommand.Any(str => command.StartsWith(str)):
                {
                    await HelloExecutor(message);
                    break;
                }
                case var _ when GoodbyeCommand.Any(str => command.StartsWith(str)):
                {
                    await GoodbyeExecutor(message);
                    break;
                }
                case var _ when RandomImageCommand.Any(str => command.StartsWith(str)):
                {
                    await RandomImageExecutor(message, command);
                    break;
                }
                case var _ when AccountAgeCommand.Any(s => command.StartsWith(s)):
                {
                    await AccountAgeExecutor(message, command, _client);
                    break;
                }
                case var _ when CoinFlipCommand.Any(s => command.StartsWith(s)):
                {
                    await CoinFlipExecutor(message);
                    break;
                }
                case var _ when ServerSiteCommand.Any(s => command.StartsWith(s)):
                {
                    await ServerSiteExecutor(message);
                    break;
                }
                case var _ when ServersIpCommand.Any(s => command.StartsWith(s)):
                {
                    await ServersIpExecutor(message);
                    break;
                }
                case var _ when ServerMirageIpCommand.Any(s => command.StartsWith(s)):
                {
                    await ServerMirageIpExecutor(message);
                    break;
                }
                case var _ when ServerAwpIpCommand.Any(s => command.StartsWith(s)):
                {
                    await ServerAwpIpExecutor(message);
                    break;
                }
            }
        }
    }
}