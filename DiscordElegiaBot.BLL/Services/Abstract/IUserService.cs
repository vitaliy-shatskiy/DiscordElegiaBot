using System.Collections.Generic;
using System.Threading.Tasks;
using DiscordElegiaBot.Common.Models.DTO.Users;
using DiscordElegiaBot.DAL.Entities;

namespace DiscordElegiaBot.BLL.Services.Abstract
{
    public interface IUserService
    {
        Task<UserInfoDto> GetUserInfoAsync(string nickname);
        Task<ICollection<UserNameWithScoreDto>> GetTopUsersByScoreAsync();
        int? GetUserTopPosition(string steamId);
        Task<UserHit> GetUserHitAsync(string steamId);
        Task<UserUnusualKill> GetUserUnusualKillsAsync(string steamId);
        Task<ICollection<UserWeapon>> GetUserWeaponsAsync(string steamId);
        Task<ICollection<UserNameWithKillsDto>> GetTopUsersByKillsAsync();
        Task<ICollection<UserNameWithDeathsDto>> GetTopUsersByDeathsAsync();
        Task<ICollection<UserNameWithTimeDto>> GetTopUsersByTimeAsync();
    }
}