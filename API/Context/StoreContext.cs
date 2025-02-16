using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Album>().HasData(new Album
            {
                Id = 1,
                Genre = "Metal",
                Description = "None",
                YearReleased = 1992,
                Artist = "Darkthrone",
                Title = "A Blaze in The Northern Sky",
                Length = 42,
            });
            modelBuilder.Entity<Album>().HasData(new Album
            {
                Id = 2,
                Genre = "Metal",
                Description = "None",
                YearReleased = 1984,
                Artist = "Metllica",
                Title = "Master of Puppets",
                Length = 55,
            });
        }
        public DbSet<User> Users {get; set;}
        public DbSet<Album> Albums { get; set; }

    }
}