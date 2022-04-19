using AthenaBackend.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AthenaBackend.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            MigrateToLatestVersionOfDB(host);

            host.Run();
        }

        private static void MigrateToLatestVersionOfDB(IHost host)
        {
            using var scope = host.Services.CreateScope();

            HandleReadContext(scope);
            HandleWriteContext(scope);
        }

        private static void HandleWriteContext(IServiceScope scope)
        {
            IDbContextFactory<WriteDbContext> writeContextFactory =
                scope.ServiceProvider.GetRequiredService<IDbContextFactory<WriteDbContext>>();
            using var writeContext = writeContextFactory.CreateDbContext();

            writeContext.Database.Migrate();
        }

        private static void HandleReadContext(IServiceScope scope)
        {
            IDbContextFactory<ReadDbContext> readContextFactory =
                 scope.ServiceProvider.GetRequiredService<IDbContextFactory<ReadDbContext>>();
            using var readContext = readContextFactory.CreateDbContext();

            readContext.Database.Migrate();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
