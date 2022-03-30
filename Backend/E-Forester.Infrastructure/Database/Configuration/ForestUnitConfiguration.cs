using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Forester.Data.Database.Configuration
{
    public class ForestUnitConfiguration : IEntityTypeConfiguration<ForestUnit>
    {
        public void Configure(EntityTypeBuilder<ForestUnit> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasIndex(b => b.Address).IsUnique();

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Address)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
