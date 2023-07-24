using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class CostCategoryEntityTypeConfiguration : IEntityTypeConfiguration<CostCategory>
{
    public void Configure(EntityTypeBuilder<CostCategory> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .HasMaxLength(200);

        builder.Property(c => c.Color)
            .IsRequired();

        builder.HasMany(c => c.UsersCosts)
            .WithOne(uc => uc.CostCategory)
            .HasForeignKey(uc => uc.CostCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}