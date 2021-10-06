using DiscordElegiaBot.DAL.Database.Context;

namespace DiscordElegiaBot.BLL.Services.Abstract.Base
{
    public abstract class BaseService
    {
        private protected readonly MirageContext _context;

        protected BaseService(MirageContext context)
        {
            _context = context;
        }
    }
}