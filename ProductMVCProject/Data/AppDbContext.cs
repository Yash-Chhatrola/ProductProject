using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ProductMVCProject.Models;
using ProductMVCProject.Models.DropDown;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductMVCProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<SaveResult> saveResults {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Country>().ToTable("Countries", "dropdowndb", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<State>().ToTable("States", "dropdowndb", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<City>().ToTable("Cities", "dropdowndb", t => t.ExcludeFromMigrations());

            base.OnModelCreating(modelBuilder);
        }
    }

}

