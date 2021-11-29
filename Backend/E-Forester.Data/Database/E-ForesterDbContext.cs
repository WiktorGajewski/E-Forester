using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;

namespace E_Forester.Data.Database
{
    public class E_ForesterDbContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<User> AppUsers { get; set; }


        public E_ForesterDbContext(DbContextOptions<E_ForesterDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Login).IsUnique();
            });
        }
    }
}
