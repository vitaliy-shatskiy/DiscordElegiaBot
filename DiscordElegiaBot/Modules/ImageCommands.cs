using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordElegiaBot.Providers;

namespace DiscordElegiaBot.Modules
{
    public class ImageCommands : ModuleBase
    {
        [Command("image")]
        [Alias("картинка", "picture", "photo")]
        public async Task RandomPicture([Remainder] string args = " ")
        {
            try
            {
                Image image;
                if (args != " ")
                    image = await RandomImageHttpProvider.GetRandomPhotoAsync(args);
                else
                    image = await RandomImageHttpProvider.GetRandomPhotoAsync();
                await Context.Channel.SendFileAsync(image.Stream, "randomPhoto.jpg");
            }
            catch
            {
                await ReplyAsync("Не могу загрузить случайную картинку :confused:");
            }
        }
    }
}