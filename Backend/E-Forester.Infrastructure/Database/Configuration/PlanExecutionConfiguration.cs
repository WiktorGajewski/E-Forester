using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Forester.Data.Database.Configuration
{
    public class PlanExecutionConfiguration : IEntityTypeConfiguration<PlanExecution>
    {
        public void Configure(EntityTypeBuilder<PlanExecution> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Plan)
                    .WithMany(b => b.PlanExecutions)
                    .HasForeignKey(b => b.PlanId);

            builder.HasOne(b => b.PlanItem)
                .WithMany(b => b.PlanExecutions)
                .HasForeignKey(b => b.PlanItemId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(b => b.Creator)
                .WithMany()
                .HasForeignKey(b => b.CreatorId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
