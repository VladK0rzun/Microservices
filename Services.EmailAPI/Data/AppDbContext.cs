using Microsoft.EntityFrameworkCore;
using Services.EmailAPI.Models;
using System.Net.Http.Headers;

namespace Services.EmailAPI.Data
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EmailLogger> EmailLoggers { get; set; }
        
    }
}
