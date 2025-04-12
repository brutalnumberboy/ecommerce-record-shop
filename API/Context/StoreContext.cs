using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace API.Context
{
    public class StoreContext : IdentityDbContext<User>
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
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/d/d3/Darkthrone_-_A_Blaze_in_the_Northern_Sky.jpg"
            });
            modelBuilder.Entity<Album>().HasData(new Album
            {
                Id = 2,
                Genre = "Metal",
                Description = "None",
                YearReleased = 1984,
                Artist = "Metallica",
                Title = "Master of Puppets",
                Length = 55,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/b2/Metallica_-_Master_of_Puppets_cover.jpg"
            });
            modelBuilder.Entity<Album>().HasData(new Album
            {
                Id = 3,
                Genre = "Metal",
                Description = "None",
                YearReleased = 1990,
                Artist = "Metallica",
                Title = "Metallica",
                Length = 62,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/2/2c/Metallica_-_Metallica_cover.jpg"
            });
            modelBuilder.Entity<Album>().HasData(new Album
            {
                Id = 4,
                Genre = "Alternative",
                Description = "None",
                YearReleased = 1993,
                Artist = "Slowdive",
                Title = "Souvlaki",
                Length = 40,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/a/a6/Souvlaki_%28album%29_cover.jpg"
            });
            modelBuilder.Entity<Album>().HasData(new Album
            {
                Id = 5,
                Genre = "Pop",
                Description = "None",
                YearReleased = 1982,
                Artist = "Michael Jackson",
                Title = "Thriller",
                Length = 42,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/5/55/Michael_Jackson_-_Thriller.png"
            });
            modelBuilder.Entity<Album>().HasData(new Album
            {
                Id = 6,
                Genre = "Metal",
                Description = "None",
                YearReleased = 1982,
                Artist = "Iron Maiden",
                Title = "The Number of the Beast",
                Length = 39,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/3/32/IronMaiden_NumberOfBeast.jpg"
            });
            modelBuilder.Entity<Album>().HasData(new Album
            {
                Id = 7,
                Genre = "Rap",
                Description = "None",
                YearReleased = 1993,
                Artist = "Wu-Tang Clan",
                Title = "Enter the Wu-Tang (36 Chambers)",
                Length = 58,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/5/53/Wu-TangClanEntertheWu-Tangalbumcover.jpg"
            });
        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<UserBasket> UserBaskets { get; set; }

    }
}