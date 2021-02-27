using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DiscordElegiaBot
{
    public class Program
    {
        private DiscordSocketClient _client;
        
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            var token = await File.ReadAllTextAsync("token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
        }

        private static Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}