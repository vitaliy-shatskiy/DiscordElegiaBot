using System;
using System.Text.RegularExpressions;
using DiscordElegiaBot.Models.Regex;

namespace DiscordElegiaBot.Extensions
{
    public static class StringExtension
    {
        public static bool IsContainUserId(this string str)
        {
            return Regex.IsMatch(str, DiscordRegex.UserIdRegEx);
        }

        public static ulong StringToLongUserId(this string str)
        {
            var match = Regex.Match(str, DiscordRegex.UserIdRegEx, RegexOptions.IgnoreCase);
            if (!match.Success)
                throw new Exception("Не могу вывести дату :confused:");
            return (ulong) Convert.ToInt64(match.Value.Substring(3, 18));
        }
    }
}