using System.Collections.Generic;

namespace DiscordElegiaBot.Materials
{
    public static class CommandsStrings
    {
        public static readonly List<string> HelloCommand = new() {"hello", "hi", "дароу", "привет", "здравствуй", "ку"};

        public static readonly List<string> GoodbyeCommand = new()
            {"bye", "Goodbye", "пока", "бб", "до свидания", "покедо", "bb"};

        public static readonly List<string> CoinFlipCommand = new()
            {"coinflip", "flip", "coin", "cf", "подкинуть монетку", "монетка"};

        public static readonly List<string> AccountAgeCommand =
            new() {"age", "возраст", "age of account", "возраст аккаунта"};

        public static readonly List<string> ServerSiteCommand = new() {"сайт", "site"};

        public static readonly List<string>
            ServerMirageIpCommand = new() {"server 1", "server1", "сервер 1", "сервер1", "мираж", "mirage"};

        public static readonly List<string> ServerAwpIpCommand =
            new() {"server 2", "server2", "сервер 2", "сервер2", "awp", "авп"};

        public static readonly List<string> ServersIpCommand = new() {"ip", "айпи"};

        public static readonly List<string> RandomImageCommand =
            new() {"image", "random image", "картинка", "случайная картинка"};
    }
}