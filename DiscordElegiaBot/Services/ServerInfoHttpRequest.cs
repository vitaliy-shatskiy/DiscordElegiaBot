using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DiscordElegiaBot.DTO.CsGoServerInfo;
using Newtonsoft.Json;

namespace DiscordElegiaBot.Services
{
    public class ServerInfoHttpRequest
    {
        public static async Task<ServerInfoDTO> GetServerInfo(string serverIp)
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri("https://elegia-project.su")
            };
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("data[0][0][ip]", serverIp)
            });
            var result =
                await client.PostAsync("/app/modules/module_block_main_servers_monitoring/includes/ServerJS.php",
                    content);
            var resultContent = await result.Content.ReadAsStringAsync();
            resultContent = resultContent.Remove(0, 1).Remove(resultContent.Length - 2, 1);
            return JsonConvert.DeserializeObject<ServerInfoDTO>(resultContent);
        }
    }
}