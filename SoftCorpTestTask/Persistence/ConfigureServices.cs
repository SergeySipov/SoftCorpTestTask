using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Common;
using Application.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Constants;
using Persistence.DbContexts;
using Persistence.Repositories;
using Persistence.Repositories.Common;

namespace Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistentServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();

        return services;
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SoftCorpTestTaskDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString(DbContextConstants.SoftCorpTestTaskDbConnectionStringName);
            options.UseSqlServer(connectionString,
                builder =>
                {
                    builder.MigrationsAssembly(typeof(SoftCorpTestTaskDbContext).Assembly.FullName);
                });

            options.UseCamelCaseNamingConvention();
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICostCategoryRepository, CostCategoryRepository>();
        services.AddScoped<IUsersCostRepository, UsersCostRepository>();
    }
}
