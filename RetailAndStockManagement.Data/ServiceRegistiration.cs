using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetailAndStockManagement.Data;
using RetailAndStockManagement.Data.EF;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration cfg)
    {
        var connectionString = cfg.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<RetailAndStockManagementContext>(opt =>
            opt.UseSqlServer(connectionString)
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddScoped<IRetailAndStockManagementContext>(sp =>
            sp.GetRequiredService<RetailAndStockManagementContext>());

        return services;
    }
}