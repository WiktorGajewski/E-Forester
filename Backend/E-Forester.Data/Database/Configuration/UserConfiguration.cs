using E_Forester.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Forester.Data.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Login)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(b => b.Login).IsUnique();

            builder.HasMany(b => b.AssignedForestUnits)
                .WithMany(b => b.AssignedUsers);

            builder.OwnsMany(b => b.RefreshTokens);
        }
    }
}
