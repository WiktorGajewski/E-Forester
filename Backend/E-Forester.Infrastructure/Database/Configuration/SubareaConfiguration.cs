using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Forester.Data.Database.Configuration
{
    public class SubareaConfiguration : IEntityTypeConfiguration<Subarea>
    {
        public void Configure(EntityTypeBuilder<Subarea> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasIndex(b => b.Address).IsUnique();

            builder.Property(b => b.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(b => b.Division)
                .WithMany(b => b.Subareas)
                .HasForeignKey(b => b.DivisionId);
        }
    }
}
