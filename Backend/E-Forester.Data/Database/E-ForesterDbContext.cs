using E_Forester.Data.Database.Configuration;
using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;

namespace E_Forester.Infrastructure.Database
{
    public class E_ForesterDbContext : DbContext
    {
        public DbSet<User> AppUsers { get; set; }
        public DbSet<ForestUnit> ForestUnits { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Subarea> Subareas { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanItem> PlanItems { get; set; }
        public DbSet<PlanExecution> PlanExecutions { get; set; }


        public E_ForesterDbContext(DbContextOptions<E_ForesterDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new ForestUnitConfiguration());

            modelBuilder.ApplyConfiguration(new DivisionConfiguration());

            modelBuilder.ApplyConfiguration(new SubareaConfiguration());

            modelBuilder.ApplyConfiguration(new PlanConfiguration());

            modelBuilder.ApplyConfiguration(new PlanItemConfiguration());

            modelBuilder.ApplyConfiguration(new PlanExecutionConfiguration());
        }
    }
}
