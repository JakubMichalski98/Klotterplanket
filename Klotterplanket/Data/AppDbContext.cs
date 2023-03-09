using Klotterplanket.Models;
using Microsoft.EntityFrameworkCore;

namespace Klotterplanket.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<MessageModel> Messages { get; set; }
    }
}
