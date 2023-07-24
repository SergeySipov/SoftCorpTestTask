using System.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.EntityConfiguration;

public class UserRefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.ExpirationDateTime)
            .IsRequired()
            .HasColumnType(SqlDbType.SmallDateTime.ToString())
            .HasDefaultValueSql(SqlServerFunctionConstants.GetUtcDate);

        builder.HasOne(urt => urt.User)
            .WithOne(u => u.UserRefreshToken)
            .HasForeignKey<UserRefreshToken>(urt => urt.UserId);
    }
}