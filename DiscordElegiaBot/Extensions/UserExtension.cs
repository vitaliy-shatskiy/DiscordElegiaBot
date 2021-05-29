using Discord;

namespace DiscordElegiaBot.Extensions
{
    public static class UserExtension
    {
        public static string GetNameWithId(this IUser user)
        {
            return $"{user.Username}#{user.Discriminator}";
        }
    }
}