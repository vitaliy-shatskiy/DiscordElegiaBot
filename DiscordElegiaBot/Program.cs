using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordElegiaBot.BLL.Providers;
using DiscordElegiaBot.BLL.Services.Abstract;
using DiscordElegiaBot.BLL.Services.ModuleServices;
using DiscordElegiaBot.DAL.Database.Context;
using DiscordElegiaBot.Services.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DiscordElegiaBot
{
    public class Program
    {
        private static string _logLevel;
        private readonly IConfiguration _config;
        private DiscordSocketClient _client;

        private Program()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("config.json");
            _config = builder.Build();
        }

        private static void Main(string[] args = null)
        {
            if (args?.Length != 0) _logLevel = args?[0];

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();


            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private async Task MainAsync()
        {
            await using var services = ConfigureServices();
            var client = services.GetRequiredService<DiscordSocketClient>();
            _client = client;
            services.GetRequiredService<LoggingService>();

            // this is where we get the Token value from the configuration file, and start the bot
            await client.LoginAsync(TokenType.Bot, _config["Token"]);
            await client.StartAsync();

            // we get the CommandHandler class here and call the InitializeAsync method to start things up for the CommandHandler service
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
            var services = new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlerService>()
                .AddScoped<RandomImageProvider>()
                .AddScoped<ServerInfoProvider>()
                .AddSingleton<LoggingService>()
                .AddDbContext<MirageContext>(builder =>
                {
                    builder.UseMySql(_config.GetConnectionString("MirageContext"),
                        new MySqlServerVersion(new Version(5, 7, 35)));
                    builder.LogTo(Log.Logger.Debug, new[] { RelationalEventId.CommandExecuted });
                })
                .AddTransient<IUserService, UserService>()
                .AddLogging(configure => configure.AddSerilog());

            if (!string.IsNullOrWhiteSpace(_logLevel))
                switch (_logLevel.ToLower())
                {
                    case "info":
                    {
                        services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
                        break;
                    }
                    case "error":
                    {
                        services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Error);
                        break;
                    }
                    case "debug":
                    {
                        services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug);
                        break;
                    }
                    default:
                    {
                        services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Error);
                        break;
                    }
                }
            else
                services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}