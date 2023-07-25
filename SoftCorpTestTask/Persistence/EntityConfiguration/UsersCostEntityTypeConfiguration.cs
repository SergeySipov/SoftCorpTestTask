using System.Data;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.EntityConfiguration;

public class UsersCostEntityTypeConfiguration : IEntityTypeConfiguration<UsersCost>
{
    public void Configure(EntityTypeBuilder<UsersCost> builder)
    {
        builder.HasKey(uc => uc.Id);

        builder.Property(uc => uc.Date)
            .IsRequired()
            .HasColumnType(SqlDbType.SmallDateTime.ToString())
            .HasDefaultValueSql(SqlServerFunctionConstants.GetUtcDate);

        builder.Property(uc => uc.Comment)
            .HasMaxLength(300);

        builder.Property(uc => uc.Price)
            .IsRequired()
            .HasPrecision(8, 6);

        builder.Property(uc => uc.CurrencyCode)
            .HasDefaultValue(CurrencyCode.BYN);

        builder.HasOne(uc => uc.CostCategory)
            .WithMany(c => c.UsersCosts)
            .HasForeignKey(uc => uc.CostCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(uc => uc.User)
            .WithMany(u => u.UsersCosts)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
