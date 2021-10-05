using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordElegiaBot.Extensions;

namespace DiscordElegiaBot.Modules
{
    public class DefaultCommands : ModuleBase
    {
        [Command("help")]
        [Alias("хелп", "помощь", "?", "info", "инфо")]
        public async Task Help()
        {
            var sb = new StringBuilder();

            sb.AppendLine("**`ip`** - ip всех серверов Elegia. _(айпи, ип, сервера, servers)_\n");
            sb.AppendLine("**`site`** - сайт Elegia. _(сайт)_\n");
            sb.AppendLine(
                "**`public`** - текущий список игроков на сервере \"PUBLIC | ELEGIA.CLUB\". _(2, pub)_\n");
            sb.AppendLine("**`mirage`** - текущий список игроков на сервере \"MIRAGE | ELEGIA.CLUB\". _(1, мираж)_\n");
            sb.AppendLine("**`hello`** - поприветствовать бота. _(hi, привет, здравствуй, ку)_\n");
            sb.AppendLine("**`demos`** - показать последние демки. _(dem, дем, демо, демка, demo, демки)_\n");
            sb.AppendLine("**`goodbye`** - попрощаться с ботом. _(пока, бб, сладких снов, bb)_\n");
            sb.AppendLine(
                "**`ask`** - магический шар-предсказать, задайте вопрос шару и он ответит вам *правду*! " +
                "После команды, через пробел задайте свой вопрос (-ask я дурак?). _(8ball, вопрос, шар)_\n");
            sb.AppendLine("**`age`** - узнать дату создания аккаунта дискорд. " +
                          "Так же если после команды упомянуть человека, вы увидите его дату создания. _(возраст)_\n");
            sb.AppendLine("**`coinflip`** - подкинуть монетку. _(flip, coin, cf, монетка)_\n");
            sb.AppendLine("**`image`** - показать случайную картинку. " +
                          "После команды можно написать тему для поиска картинки, только на английском (-image ocean). _(картинка, picture, photo)_\n");
            sb.AppendLine("**`help`** - список доступных команд бота. _(хелп, помощь, ?, info, инфо)_\n");
            sb.AppendLine("**`null`** - ???\n");
            sb.AppendLine($"Автор бота -> [{Context.Client.GetUserAsync(245938852668112897).Result.Mention}]");

            var embed = new EmbedBuilder
            {
                Title = "Список доступных команд бота",
                Color = Color.LightOrange,
                Description = sb.ToString()
            };
            await ReplyAsync(null, false, embed.Build());
        }


        [Command("hello")]
        [Alias("hi", "привет", "здравствуй", "ку")]
        public async Task Hello()
        {
            var sb = new StringBuilder();
            var user = Context.User;
            sb.AppendLine($"Привет :grin: {user.Mention}");
            await ReplyAsync(sb.ToString());
        }

        [Command("goodbye")]
        [Alias("пока", "бб", "сладких снов", "bb")]
        public async Task Goodbye()
        {
            var sb = new StringBuilder();
            var user = Context.User;
            sb.AppendLine($"Сладких снов :kissing_heart: {user.Mention}");
            await ReplyAsync(sb.ToString());
        }

        [Command("8ball")]
        [Alias("ask", "вопрос", "шар")]
        public async Task AskEightBall([Remainder] string args = null)
        {
            var sb = new StringBuilder();
            var embed = new EmbedBuilder();
            var replies = new List<string>
            {
                "да", "очень вероятно", "нет", "непонятно", "мало шансов", "шансы высокие", "точно да",
                "духи говорят да", "спросите снова", "возможно", "думаю нет", "не могу сказать", "без сомнений",
                "неясно", "точно нет", "духи говорят нет", "думаю да"
            };
            embed.WithColor(new Color(0, 255, 0));
            embed.Title = "Я великий МАГИЧЕСКИЙ ШАР";
            sb.AppendLine($"{Context.User.Username},");
            sb.AppendLine();
            if (args == null)
            {
                sb.AppendLine("Извини, я не могу ответить на вопрос которого нет!");
            }
            else
            {
                var answer = replies[new Random().Next(replies.Count)];
                sb.AppendLine($"Ты спросил: [**{args}**]...");
                sb.AppendLine();
                sb.AppendLine($".... ответ: [**{answer}!**]");
                switch (answer)
                {
                    case "да":
                    case "очень вероятно":
                    case "точно да":
                    case "духи говорят да":
                    case "без сомнений":
                    case "думаю да":
                    {
                        embed.WithColor(new Color(0, 255, 0));
                        break;
                    }
                    case "нет":
                    case "мало шансов":
                    case "духи говорят нет":
                    case "точно нет":
                    case "думаю нет":
                    {
                        embed.WithColor(new Color(255, 0, 0));
                        break;
                    }
                    default:
                    {
                        embed.WithColor(new Color(255, 255, 0));
                        break;
                    }
                }
            }

            embed.Description = sb.ToString();
            await ReplyAsync(null, false, embed.Build());
        }


        [Command("age")]
        [Alias("возраст")]
        public async Task AccountAge([Remainder] string args = "")
        {
            var sb = new StringBuilder();
            var user = args.IsContainUserId()
                ? Context.Client.GetUserAsync(args.StringToLongUserId()).Result
                : Context.User;
            Console.WriteLine($"[{args}]");
            sb.Append($"Аккаунт {user.Mention} создан: {user.CreatedAt.DateTime}");
            await ReplyAsync(sb.ToString());
        }

        [Command("coinflip")]
        [Alias("flip", "coin", "cf", "монетка")]
        public async Task CoinFlip()
        {
            var sb = new StringBuilder();
            Random random = new();
            var replies = new List<string> {"Решка", "Орёл"};
            var answer = replies[random.NextDouble() < 0.5 ? 0 : 1];
            var embed = new EmbedBuilder();
            switch (answer)
            {
                case "Решка":
                    embed.WithColor(new Color(1, 1, 1));
                    break;
                case "Орёл":
                    embed.WithColor(new Color(255, 255, 255));
                    break;
            }

            embed.Description = answer;
            await ReplyAsync(null, false, embed.Build());
        }
    }
}