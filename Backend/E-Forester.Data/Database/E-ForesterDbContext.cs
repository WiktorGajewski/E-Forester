using Microsoft.EntityFrameworkCore;

namespace E_Forester.Data.Database
{
    public class E_ForesterDbContext : DbContext
    {
        public E_ForesterDbContext(DbContextOptions<E_ForesterDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
