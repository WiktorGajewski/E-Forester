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

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Login).IsUnique();

                entity.HasMany(e => e.AssignedForestUnits)
                    .WithMany(e => e.AssignedUsers);

                entity.OwnsMany(e => e.RefreshTokens);
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.HasOne(e => e.ForestUnit)
                    .WithMany(e => e.Divisions)
                    .HasForeignKey(e => e.ForestUnitId);
            });

            modelBuilder.Entity<Subarea>(entity =>
            {
                entity.HasOne(e => e.Division)
                    .WithMany(e => e.Subareas)
                    .HasForeignKey(e => e.DivisionId);
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasIndex(e => new { e.ForestUnitId, e.Year}).IsUnique();

                entity.HasOne(e => e.ForestUnit)
                    .WithMany(e => e.Plans)
                    .HasForeignKey(e => e.ForestUnitId);

                entity.HasOne(e => e.Creator)
                    .WithMany()
                    .HasForeignKey(e => e.CreatorId);
            });

            modelBuilder.Entity<PlanItem>(entity =>
            {
                entity.HasIndex(e => new { e.PlanId, e.SubareaId, e.ActionGroup }).IsUnique();

                entity.HasOne(e => e.Plan)
                    .WithMany(e => e.PlanItems)
                    .HasForeignKey(e => e.PlanId);
                    

                entity.HasOne(e => e.Subarea)
                    .WithMany(e => e.PlanItems)
                    .HasForeignKey(e => e.SubareaId)
                    .OnDelete(DeleteBehavior.ClientNoAction);

                entity.HasOne(e => e.Creator)
                    .WithMany()
                    .HasForeignKey(e => e.CreatorId)
                    .OnDelete(DeleteBehavior.ClientNoAction);
            });

            modelBuilder.Entity<PlanExecution>(entity =>
            {
                entity.HasOne(e => e.Plan)
                    .WithMany(e => e.PlanExecutions)
                    .HasForeignKey(e => e.PlanId);
                    

                entity.HasOne(e => e.PlanItem)
                    .WithMany(e => e.PlanExecutions)
                    .HasForeignKey(e => e.PlanItemId)
                    .OnDelete(DeleteBehavior.ClientNoAction);

                entity.HasOne(e => e.Creator)
                    .WithMany()
                    .HasForeignKey(e => e.CreatorId)
                    .OnDelete(DeleteBehavior.ClientNoAction);
            });
        }
    }
}
