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
            .IsRequired()
            .HasMaxLength(7);

        builder.HasMany(c => c.UsersCosts)
            .WithOne(uc => uc.CostCategory)
            .HasForeignKey(uc => uc.CostCategoryId)
            .OnDelete(DeleteBehavior.Restrict);


        var defaultCostCategories = new List<CostCategory>
        {
            new CostCategory
            {
                Id = 1,
                Name = "Groceries",
                Color = "#eb4034",
                Description = null
            },
            new CostCategory
            {
                Id = 2,
                Name = "Transport",
                Color = "#ebde34",
                Description = null
            },
            new CostCategory
            {
                Id = 3,
                Name = "Mobile Communication",
                Color = "#4feb34",
                Description = null
            },
            new CostCategory
            {
                Id = 4,
                Name = "Internet",
                Color = "#1732ff",
                Description = null
            },
            new CostCategory
            {
                Id = 5,
                Name = "Entertainment",
                Color = "#6017ff",
                Description = null
            }
        };

        builder.HasData(defaultCostCategories);
    }
}