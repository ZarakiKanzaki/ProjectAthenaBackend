using Microsoft.EntityFrameworkCore;

namespace AthenaBackend.Infrastructure
{
    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }
    }
}