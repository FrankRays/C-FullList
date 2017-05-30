using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace beltReviewer.Models
{
    public class YourContext : DbContext
    {

        
        public YourContext(DbContextOptions<YourContext> options) : base(options){}
        public DbSet<User> newUser {get;set;}
        public DbSet<Car> Cars {get;set;}
        public DbSet<Make> make {get;set;}
        public DbSet<Model> model {get;set;}
        public DbSet<Rental> Rentals {get;set;}

        
    }
}