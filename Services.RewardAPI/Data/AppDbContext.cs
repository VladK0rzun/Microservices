using Microsoft.EntityFrameworkCore;
using Services.RewardAPI.Models;


namespace Services.RewardAPI.Data
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Rewards> Rewards { get; set; }
        


        
    }
}
