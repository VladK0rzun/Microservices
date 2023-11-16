using Microsoft.EntityFrameworkCore;
using Services.OrderAPI.Models;
using System.Net.Http.Headers;

namespace Services.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        
    }
}
