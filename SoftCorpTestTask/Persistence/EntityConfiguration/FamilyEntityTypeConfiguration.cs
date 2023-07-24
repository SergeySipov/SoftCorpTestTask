using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class FamilyEntityTypeConfiguration : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(f => f.UsersFamilies)
            .WithOne(uf => uf.Family)
            .HasForeignKey(uf => uf.FamilyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}