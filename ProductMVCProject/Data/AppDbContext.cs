using Microsoft.EntityFrameworkCore;
using ProductMVCProject.Models;

namespace ProductMVCProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
    }
}
