using System;
using System.Text;
using System.Text.RegularExpressions;
using DiscordElegiaBot.Common.Models.Regex;

namespace DiscordElegiaBot.Common.Extensions
{
    public static class StringExtension
    {
        public static bool IsContainUserId(this string str)
        {
            return Regex.IsMatch(str, DiscordRegex.UserIdRegex);
        }

        public static bool IsContainSteamId(this string str)
        {
            return Regex.IsMatch(str, SteamRegex.SteamIdRegex);
        }

        public static string ConvertSteamIdForDb(this string str)
        {
            if (str.IsContainSteamId()) return new StringBuilder(str) { ["STEAM_".Length] = '1' }.ToString();
            throw new Exception("Не могу записать SteamID :confused:");
        }

        public static ulong StringToLongUserId(this string str)
        {
            var match = Regex.Match(str, DiscordRegex.UserIdRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
                throw new Exception("Не могу вывести дату :confused:");
            return (ulong)Convert.ToInt64(match.Value.Substring(3, 18));
        }
    }
}