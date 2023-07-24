using Application.Interfaces.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Domain.Entities;

namespace Persistence.DbContexts;

public class SoftCorpTestTaskDbContext : DbContext, ISoftCorpTestTaskDbContext
{
    public SoftCorpTestTaskDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CostCategory> CostCategories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UsersCost> UsersCosts { get; set; }
    public DbSet<Family> Families { get; set; }
    public DbSet<UserFamily> UsersFamilies { get; set; }
    public DbSet<UserRefreshToken> UsersRefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}