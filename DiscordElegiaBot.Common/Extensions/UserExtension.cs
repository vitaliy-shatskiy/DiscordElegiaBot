using Discord;

namespace DiscordElegiaBot.Common.Extensions
{
    public static class UserExtension
    {
        public static string GetNameWithId(this IUser user)
        {
            return $"{user.Username}#{user.Discriminator}";
        }
    }
}