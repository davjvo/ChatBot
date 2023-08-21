using DatChatBot.DataLayer;

namespace DavChatBot.Services
{
    public abstract class BaseDbServices<T>
    {
        protected readonly DavChatBotDbContext _dbContext;

        public BaseDbServices(DavChatBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

