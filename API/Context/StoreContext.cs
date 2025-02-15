using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Album> album {get; set;} 
    }
}