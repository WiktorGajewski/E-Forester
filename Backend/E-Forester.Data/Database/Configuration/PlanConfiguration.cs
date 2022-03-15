using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Forester.Data.Database.Configuration
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasIndex(b => new { b.ForestUnitId, b.Year }).IsUnique();

            builder.HasOne(b => b.ForestUnit)
                .WithMany(b => b.Plans)
                .HasForeignKey(b => b.ForestUnitId);

            builder.HasOne(b => b.Creator)
                .WithMany()
                .HasForeignKey(b => b.CreatorId);
        }
    }
}
