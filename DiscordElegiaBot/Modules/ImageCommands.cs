using System.Threading.Tasks;
using Discord.Commands;
using DiscordElegiaBot.Providers;

namespace DiscordElegiaBot.Modules
{
    public class ImageCommands : ModuleBase
    {
        [Command("image")]
        [Alias("картинка", "picture", "photo")]
        public async Task RandomPicture([Remainder] string args = null)
        {
            try
            {
                var image = args == null
                    ? await RandomImageHttpProvider.GetRandomPhotoAsync(args)
                    : await RandomImageHttpProvider.GetRandomPhotoAsync();
                await Context.Channel.SendFileAsync(image.Stream, "randomPhoto.jpg");
            }
            catch
            {
                await ReplyAsync("Не могу загрузить случайную картинку :confused:");
            }
        }
    }
}