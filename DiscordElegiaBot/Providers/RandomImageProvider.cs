using System;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;

namespace DiscordElegiaBot.Providers
{
    public class RandomImageProvider
    {
        private const string ImageServiceUri = "https://source.unsplash.com";

        public async Task<Image> GetRandomPhotoAsync(string image = "")
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri(ImageServiceUri)
            };
            var result = await client.GetAsync($"/random/?{image}");
            return new Image(await result.Content.ReadAsStreamAsync());
        }
    }
}