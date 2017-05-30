using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace EntityQuotes.Models
{
    public class YourContext : DbContext
    {

        
        public YourContext(DbContextOptions<YourContext> options) : base(options){}
        public DbSet<Quote> quotes {get;set;}
        
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     string Server = "localhost";
        //     string Port = "8889"; //or 8889 on Mac
        //     string Database = "NetConnect";
        //     string UserId = "root";
        //     string Password = "root";
        //     string Connection = $"Server={Server};port={Port};database={Database};uid={UserId};pwd={Password};SslMode=None;";
        //     optionsBuilder.UseMySQL(Connection);
        // }
    }
}