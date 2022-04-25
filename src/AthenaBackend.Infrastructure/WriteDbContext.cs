using AthenaBackend.Domain.Core.Characters;
using AthenaBackend.Domain.Core.Themebooks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AthenaBackend.Infrastructure
{
    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Themebook> Themebooks { get; set; }
        public virtual DbSet<Character> Characters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlite("ConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}