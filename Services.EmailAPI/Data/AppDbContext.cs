using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Services.EmailAPI.Data
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        
    }
}
