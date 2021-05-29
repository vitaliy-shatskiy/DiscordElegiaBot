using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordElegiaBot.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordElegiaBot.Services
{
    public class CommandHandlerService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _services;

        public CommandHandlerService(IServiceProvider services)
        {
            _config = services.GetRequiredService<IConfiguration>();
            _commands = services.GetRequiredService<CommandService>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _services = services;

            _commands.CommandExecuted += CommandExecutedAsync;

            // take action when we receive a message (so we can process it, and see if it is a valid command)
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        public async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            // ensures we don't process system/other bot messages
            if (rawMessage is not SocketUserMessage message) return;

            if (message.Source != MessageSource.User) return;

            var argumentPosition = 0;
            var prefix = char.Parse(_config["Prefix"]);
            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argumentPosition) ||
                  message.HasCharPrefix(prefix, ref argumentPosition)))
                return;

            var context = new SocketCommandContext(_client, message);
            await _commands.ExecuteAsync(context, argumentPosition, _services);
        }

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!command.IsSpecified)
            {
                Console.WriteLine($"Команда с ошибкой у [{context.User.GetNameWithId()}] - [{result.ErrorReason}]!");
                return;
            }

            if (result.IsSuccess)
            {
                Console.WriteLine($"Команда [{command.Value.Name}] выполнена для -> [{context.User.GetNameWithId()}]");
                return;
            }

            await context.Channel.SendMessageAsync(
                $"Упс, {context.User.Mention}... что-то пошло не так -> [{result}]!");
        }
    }
}