using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AthenaBackend.Infrastructure
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {

        }

        public virtual DbSet<ThemebookUI> ThemebookUI { get; set; }
        public virtual DbSet<ThemebookConceptUI> ThemebookConceptUI { get; set; }
        public virtual DbSet<ThemebookImprovementUI> ThemebookImprovementUI { get; set; }
        public virtual DbSet<TagQuestionUI> TagQuestionUI { get; set; }


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