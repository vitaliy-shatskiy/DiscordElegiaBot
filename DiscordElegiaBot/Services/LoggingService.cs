using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiscordElegiaBot.Services
{
    public class LoggingService
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _discord;
        private readonly ILogger _logger;

        public LoggingService(IServiceProvider services)
        {
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _commands = services.GetRequiredService<CommandService>();
            _logger = services.GetRequiredService<ILogger<LoggingService>>();


            _discord.Ready += OnReadyAsync;
            _discord.Log += OnLogAsync;
            _commands.Log += OnLogAsync;
        }

        private Task OnReadyAsync()
        {
            _logger.LogInformation($"Connected as -> [{_discord.CurrentUser}] :)");
            _logger.LogInformation($"We are on [{_discord.Guilds.Count}] servers");
            return Task.CompletedTask;
        }

        private Task OnLogAsync(LogMessage msg)
        {
            var logText = $"{msg.Source}: {msg.Exception?.ToString() ?? msg.Message}";
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                    _logger.LogCritical(logText);
                    break;
                case LogSeverity.Error:
                    break;
                case LogSeverity.Warning:
                    _logger.LogWarning(logText);
                    break;
                case LogSeverity.Info:
                    _logger.LogInformation(logText);
                    break;
                case LogSeverity.Verbose:
                    _logger.LogInformation(logText);
                    break;
                case LogSeverity.Debug:
                    _logger.LogDebug(logText);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(msg));
            }

            return Task.CompletedTask;
        }
    }
}