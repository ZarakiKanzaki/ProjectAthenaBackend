using Microsoft.EntityFrameworkCore;

namespace AthenaBackend.Infrastructure
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }
    }
}