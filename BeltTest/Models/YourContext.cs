using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace BeltTest.Models
{
    public class YourContext : DbContext
    {

        
        public YourContext(DbContextOptions<YourContext> options) : base(options){}
        public DbSet<User> Users {get;set;}
        
        public DbSet<Auction> Auctions {get;set;}

        
    }
}