using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AadFriendsPicker.Data
{
    public class FriendsDBContext : DbContext
    {
        public FriendsDBContext(DbContextOptions<FriendsDBContext> options) : base(options) { }
        public DbSet<Friends> Friends { get; set; }
        public async Task<Friends> AddFriend(Friends newFriend)
        {
            Friends.Add(newFriend);
            await SaveChangesAsync();
            return newFriend;
        }
        public async Task RemoveFriend(Friends friend)
        {
            Friends.Remove(friend);
            await SaveChangesAsync();
        }
    }
    class FriendsContextFactory : IDesignTimeDbContextFactory<FriendsDBContext>
    {
        public FriendsDBContext CreateDbContext(string[]? args = null)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<FriendsDBContext>();
            optionsBuilder
                // Uncomment the following line if you want to print generated
                // SQL statements on the console.
                // .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            return new FriendsDBContext(optionsBuilder.Options);
        }
    }
}
