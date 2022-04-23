using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace AthenaBackend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("default");

            services.AddPooledDbContextFactory<ReadDbContext>(
                            (s, o) => o.UseSqlite(connectionString)
                                        .UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()));


            services.AddPooledDbContextFactory<WriteDbContext>(
                            (s, o) => o.UseSqlite(connectionString)
                                        .UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()));

            return services;
        }
    }
}
