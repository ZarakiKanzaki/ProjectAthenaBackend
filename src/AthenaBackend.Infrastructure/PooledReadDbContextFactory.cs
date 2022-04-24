using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AthenaBackend.Infrastructure
{
    public class PooledReadDbContextFactory : IDbContextFactory<ReadDbContext>
    {
        private readonly IDbContextFactory<ReadDbContext> pooledFactory;

        public PooledReadDbContextFactory(
            IDbContextFactory<ReadDbContext> pooledFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.pooledFactory = pooledFactory;
        }

        public ReadDbContext CreateDbContext()
        {
            var context = pooledFactory.CreateDbContext();

            return context;
        }
    }
}
