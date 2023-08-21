using DatChatBot.DataLayer.Entities;
using DatChatBot.DataLayer.Maps;
using DatChatBot.DataLayer.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatChatBot.DataLayer
{

    public class DavChatBotDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<ChatBotMessage> ChatBotMessages { get; set; }

        public DavChatBotDbContext(DbContextOptions<DavChatBotDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .AddIdentityMap()
                .AddChatBotSeed()
                .ApplyConfigurationsFromAssembly(typeof(DavChatBotDbContext).Assembly);
        }
    }
}

