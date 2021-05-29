using System;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;

namespace DiscordElegiaBot.Providers
{
    public static class RandomImageHttpProvider
    {
        public static async Task<Image> GetRandomPhotoAsync(string image = "")
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri("https://source.unsplash.com")
            };
            var result = await client.GetAsync($"/random/?{image}");
            return new Image(await result.Content.ReadAsStreamAsync());
        }
    }
}