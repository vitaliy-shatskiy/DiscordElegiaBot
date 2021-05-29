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

        public Program()
        {
            // create the configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("config.json");

            // build the configuration and assign to _config          
            _config = builder.Build();
        }

        private static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            using (var services = ConfigureServices())
            {
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
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"Connected as -> [{_client.CurrentUser}] :)");
            return Task.CompletedTask;
        }

        // this method handles the ServiceCollection creation/configuration, and builds out the service provider we can call on later
        private ServiceProvider ConfigureServices()
        {
            // this returns a ServiceProvider that is used later to call for those services
            // we can add types we have access to here, hence adding the new using statement:
            // using csharpi.Services;
            // the config we build is also added, which comes in handy for setting the command prefix!
            return new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlerService>()
                .BuildServiceProvider();
        }

        // public async Task MainAsync()
        // {
        //     _client = new DiscordSocketClient();
        //     _client.MessageReceived += CommandHandler;
        //     _client.Log += Log;
        //     var token = await File.ReadAllTextAsync("token.txt");
        //
        //     await _client.LoginAsync(TokenType.Bot, token);
        //     await _client.StartAsync();
        //     await Task.Delay(-1);
        // }
        //
        // private static Task Log(LogMessage message)
        // {
        //     Console.WriteLine(message.ToString());
        //     return Task.CompletedTask;
        // }

        // private async Task CommandHandler(SocketMessage message)
        // {
        //     if (!message.Content.StartsWith('-'))
        //         return;
        //     if (message.Author.IsBot)
        //         return;
        //     var command = message.Content.Remove(0, 1).ToLower();
        //     switch (command)
        //     {
        //         case var _ when HelloCommand.Any(str => command.StartsWith(str)):
        //         {
        //             await HelloExecutor(message);
        //             break;
        //         }
        //         case var _ when GoodbyeCommand.Any(str => command.StartsWith(str)):
        //         {
        //             await GoodbyeExecutor(message);
        //             break;
        //         }
        //         case var _ when RandomImageCommand.Any(str => command.StartsWith(str)):
        //         {
        //             await RandomImageExecutor(message, command);
        //             break;
        //         }
        //         case var _ when AccountAgeCommand.Any(s => command.StartsWith(s)):
        //         {
        //             await AccountAgeExecutor(message, command, _client);
        //             break;
        //         }
        //         case var _ when CoinFlipCommand.Any(s => command.StartsWith(s)):
        //         {
        //             await CoinFlipExecutor(message);
        //             break;
        //         }
        //         case var _ when ServerSiteCommand.Any(s => command.StartsWith(s)):
        //         {
        //             await ServerSiteExecutor(message);
        //             break;
        //         }
        //         case var _ when ServersIpCommand.Any(s => command.StartsWith(s)):
        //         {
        //             await ServersIpExecutor(message);
        //             break;
        //         }
        //         case var _ when ServerMirageIpCommand.Any(s => command.StartsWith(s)):
        //         {
        //             await ServerMirageIpExecutor(message);
        //             break;
        //         }
        //         case var _ when ServerAwpIpCommand.Any(s => command.StartsWith(s)):
        //         {
        //             await ServerAwpIpExecutor(message);
        //             break;
        //         }
        //     }
        // }
    }
}