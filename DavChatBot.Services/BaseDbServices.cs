using DatChatBot.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace DavChatBot.Services
{
    public abstract class BaseDbServices<T>
    {
        protected readonly DavChatBotDbContext _dbContext;

        public BaseDbServices(IDbContextFactory<DavChatBotDbContext> dbContextFactory)
        {
            _dbContext = dbContextFactory.CreateDbContext();
        }
    }
}

