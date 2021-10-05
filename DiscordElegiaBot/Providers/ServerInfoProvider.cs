using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DiscordElegiaBot.Models.Configurations;
using DiscordElegiaBot.Models.DTO.CsGoServerInfo;
using FluentFTP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DiscordElegiaBot.Providers
{
    public class ServerInfoProvider
    {
        private readonly Config _config;

        public ServerInfoProvider(IServiceProvider serviceProvider)
        {
            _config = new Config();
            serviceProvider.GetRequiredService<IConfiguration>().Bind(_config);
        }

        public async Task<ServerInfoDTO> GetServerInfoAsync(string serverIp)
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri(_config.ServerSiteUrl)
            };
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("data[0][0][ip]", serverIp)
            });
            var result =
                await client.PostAsync("/app/modules/module_block_main_servers_monitoring/includes/js_controller.php",
                    content);
            var resultContent = await result.Content.ReadAsStringAsync();
            resultContent = resultContent.Remove(0, 1).Remove(resultContent.Length - 2, 1);
            return JsonConvert.DeserializeObject<ServerInfoDTO>(resultContent);
        }

        public async Task<ICollection<FtpListItem>> GetMirageDemos()
        {
            using var client = new FtpClient
            {
                Host = _config.Mirage.FtpHost,
                Credentials = new NetworkCredential(_config.Mirage.FtpUser, _config.Mirage.FtpPass)
            };
            try
            {
                await client.ConnectAsync();
                if (!client.IsConnected) throw new Exception("Mirage FTP connect failure");

                var list = await client.GetListingAsync();
                return list.Where(item => item.Name.Contains(".dem"))
                    .OrderByDescending(item => item.Modified)
                    .Take(10)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                await client.DisconnectAsync();
                if (!client.IsDisposed) client.Dispose();
            }
        }
    }
}