using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordElegiaBot.BLL.Services.Abstract;
using DiscordElegiaBot.BLL.Services.Abstract.Base;
using DiscordElegiaBot.Common.Extensions;
using DiscordElegiaBot.Common.Models.DTO.Users;
using DiscordElegiaBot.DAL.Database.Context;
using DiscordElegiaBot.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordElegiaBot.BLL.Services.ModuleServices
{
    public class UserService : BaseService, IUserService
    {
        public UserService(MirageContext context) : base(context)
        {
        }

        public async Task<UserInfoDto> GetUserInfoAsync(string nickname)
        {
            var u = new User();
            if (nickname.IsContainSteamId())
            {
                var steam = nickname.ConvertSteamIdForDb();
                u = await _context.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(user => user.Steam == steam)
                    ?? throw new KeyNotFoundException("Игрок не найден!");
            }
            else
            {
                var users = await _context.Users
                    .AsNoTracking()
                    .Where(user => user.Name == nickname)
                    .ToListAsync();

                switch (users.Count)
                {
                    case 1:
                        u = users.First();
                        break;
                    case > 1:
                    {
                        var sb = new StringBuilder();
                        foreach (var user in users) sb.AppendLine($"[SteamID](https://steamid.xyz/{user.Steam})");
                        throw new DuplicateNameException("Найдено больше одного игрока с таким ником\n" +
                                                         "Выберите ваш профиль и введите команду уже с вашим SteamId\n" +
                                                         sb);
                    }
                    case 0:
                        throw new KeyNotFoundException("Игрок не найден!");
                }
            }

            return new UserInfoDto
            {
                User = u,
                Top = (GetUserTopPosition(u.Steam) ?? -1) + 1,
                UserHit = await GetUserHitAsync(u.Steam),
                UserUnusualKill = await GetUserUnusualKillsAsync(u.Steam),
                UserWeapons = await GetUserWeaponsAsync(u.Steam)
            };
        }

        public async Task<ICollection<UserNameWithScoreDto>> GetTopUsersByScoreAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .OrderByDescending(user => user.Value)
                .Take(30)
                .Select(user => new UserNameWithScoreDto
                {
                    UserName = user.Name,
                    Score = user.Value
                })
                .ToListAsync();
        }

        public int? GetUserTopPosition(string steamId)
        {
            return _context.Users
                .AsNoTracking()
                .OrderByDescending(user => user.Value)
                .Select(user => user.Steam)
                .AsEnumerable()
                .Select((steam, topIndex) => new { steam, topIndex })
                .FirstOrDefault(arg => arg.steam == steamId)
                ?.topIndex;
        }

        public async Task<UserHit> GetUserHitAsync(string steamId)
        {
            return await _context.UsersHits
                .AsNoTracking()
                .FirstOrDefaultAsync(hit => hit.SteamId == steamId);
        }

        public async Task<UserUnusualKill> GetUserUnusualKillsAsync(string steamId)
        {
            return await _context.UsersUnusualKills
                .AsNoTracking()
                .FirstOrDefaultAsync(kill => kill.SteamId == steamId);
        }

        public async Task<ICollection<UserWeapon>> GetUserWeaponsAsync(string steamId)
        {
            return await _context.UsersWeapons
                .AsNoTracking()
                .Where(weapon => weapon.Steam == steamId)
                .OrderByDescending(weapon => weapon.Kills)
                .Take(5)
                .ToListAsync();
        }

        public async Task<ICollection<UserNameWithKillsDto>> GetTopUsersByKillsAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .OrderByDescending(user => user.Kills)
                .Take(30)
                .Select(user => new UserNameWithKillsDto
                {
                    UserName = user.Name,
                    Kills = user.Kills
                })
                .ToListAsync();
        }

        public async Task<ICollection<UserNameWithDeathsDto>> GetTopUsersByDeathsAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .OrderByDescending(user => user.Deaths)
                .Take(30)
                .Select(user => new UserNameWithDeathsDto
                {
                    UserName = user.Name,
                    Deaths = user.Deaths
                })
                .ToListAsync();
        }

        public async Task<ICollection<UserNameWithTimeDto>> GetTopUsersByTimeAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .OrderByDescending(user => user.Playtime)
                .Take(30)
                .Select(user => new UserNameWithTimeDto
                {
                    UserName = user.Name,
                    Playtime = user.Playtime
                })
                .ToListAsync();
        }
    }
}