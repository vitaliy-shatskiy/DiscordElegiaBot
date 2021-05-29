using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordElegiaBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordElegiaBot
{
    public class Program
    {
        private readonly IConfiguration _config;
        private DiscordSocketClient _client;

        private Program()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("config.json");
            _config = builder.Build();
        }

        private static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private async Task MainAsync()
        {
            await using var services = ConfigureServices();
            var client = services.GetRequiredService<DiscordSocketClient>();
            _client = client;
            client.Log += LogAsync;
            client.Ready += ReadyAsync;
            services.GetRequiredService<CommandService>().Log += LogAsync;
            await client.LoginAsync(TokenType.Bot, _config["Token"]);
            await client.StartAsync();
            await services.GetRequiredService<CommandHandlerService>().InitializeAsync();
            await Task.Delay(-1);
        }

        private static Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"Connected as -> [{_client.CurrentUser}] :)");
            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlerService>()
                .BuildServiceProvider();
        }
    }
}