using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Forester.Data.Database.Configuration
{
    public class DivisionConfiguration : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasIndex(b => b.Address).IsUnique();

            builder.Property(b => b.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(b => b.ForestUnit)
                .WithMany(b => b.Divisions)
                .HasForeignKey(b => b.ForestUnitId);
        }
    }
}
