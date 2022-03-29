using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Forester.Data.Database.Configuration
{
    public class PlanItemConfiguration : IEntityTypeConfiguration<PlanItem>
    {
        public void Configure(EntityTypeBuilder<PlanItem> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasIndex(b => new { b.PlanId, b.SubareaId, b.ActionGroup }).IsUnique();

            builder.HasOne(b => b.Plan)
                .WithMany(b => b.PlanItems)
                .HasForeignKey(b => b.PlanId);


            builder.HasOne(b => b.Subarea)
                .WithMany(b => b.PlanItems)
                .HasForeignKey(b => b.SubareaId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(b => b.Creator)
                .WithMany()
                .HasForeignKey(b => b.CreatorId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
