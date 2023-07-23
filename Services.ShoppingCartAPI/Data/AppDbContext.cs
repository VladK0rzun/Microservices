using Microsoft.EntityFrameworkCore;
using Services.ShoppingCartAPI.Models;
using System.Net.Http.Headers;

namespace Services.ShoppingCartAPI.Data
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        
    }
}
