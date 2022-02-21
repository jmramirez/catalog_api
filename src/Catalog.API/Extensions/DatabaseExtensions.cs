using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddCatalogContext(this IServiceCollection services, string connectionString)
    {
        return services
            .AddDbContext<CatalogContext>(contextOptions =>
            {
                contextOptions.UseNpgsql(connectionString, serverOptions =>
                {
                    serverOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
                });
            });
    }
}