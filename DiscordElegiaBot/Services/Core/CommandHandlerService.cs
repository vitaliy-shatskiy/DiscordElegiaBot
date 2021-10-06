using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiscordElegiaBot.Services.Core
{
    public class CommandHandlerService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IServiceProvider _services;

        public CommandHandlerService(IServiceProvider services)
        {
            _config = services.GetRequiredService<IConfiguration>();
            _commands = services.GetRequiredService<CommandService>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _logger = services.GetRequiredService<ILogger<CommandHandlerService>>();
            _services = services;

            _commands.CommandExecuted += CommandExecutedAsync;
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        // Message receiver
        private async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            if (rawMessage is not SocketUserMessage message) return;

            if (message.Source != MessageSource.User) return;

            var argPos = 0;
            var prefix = char.Parse(_config["Prefix"]);

            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) ||
                  message.HasCharPrefix(prefix, ref argPos)))
                return;

            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(context, argPos, _services);
        }

        private async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!command.IsSpecified)
            {
                _logger.LogError(
                    $"Command failed to execute for [{context.User.Username}] <-> [{result.ErrorReason}]!");
                return;
            }

            if (result.IsSuccess)
            {
                _logger.LogInformation(
                    $"Command [{command.Value.Name}] executed for [{context.User.Username}] on [{context.Guild.Name}]");
                return;
            }

            await context.Channel.SendMessageAsync(
                $"Sorry, {context.User.Username}... something went wrong -> [{result}]!");
        }
    }
}