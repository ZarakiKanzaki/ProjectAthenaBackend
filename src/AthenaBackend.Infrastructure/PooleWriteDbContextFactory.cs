using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AthenaBackend.Infrastructure
{
    public class PooledWriteDbContextFactory : IDbContextFactory<WriteDbContext>
    {
        private readonly IDbContextFactory<WriteDbContext> pooledFactory;

        public PooledWriteDbContextFactory(
            IDbContextFactory<WriteDbContext> pooledFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.pooledFactory = pooledFactory;
        }

        public WriteDbContext CreateDbContext()
        {
            var context = pooledFactory.CreateDbContext();

            return context;
        }
    }
}
