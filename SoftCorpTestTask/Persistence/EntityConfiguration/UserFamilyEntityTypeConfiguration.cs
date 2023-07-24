using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class UserFamilyEntityTypeConfiguration : IEntityTypeConfiguration<UserFamily>
{
    public void Configure(EntityTypeBuilder<UserFamily> builder)
    {
        builder.HasKey(uf => uf.Id);
    }
}
