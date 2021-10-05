using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordElegiaBot.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordElegiaBot.Modules
{
    public class ImageCommands : ModuleBase
    {
        private readonly RandomImageProvider _randomImageProvider;

        public ImageCommands(IServiceProvider services)
        {
            _randomImageProvider = services.GetRequiredService<RandomImageProvider>();
        }

        [Command("image")]
        [Alias("картинка", "picture", "photo")]
        public async Task RandomPicture([Remainder] string args = " ")
        {
            try
            {
                Image image;
                if (args != " ")
                    image = await _randomImageProvider.GetRandomPhotoAsync(args);
                else
                    image = await _randomImageProvider.GetRandomPhotoAsync();
                await Context.Channel.SendFileAsync(image.Stream, "randomPhoto.jpg");
            }
            catch
            {
                await ReplyAsync("Не могу загрузить случайную картинку :confused:");
            }
        }
    }
}