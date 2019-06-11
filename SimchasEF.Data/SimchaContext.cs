using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SimchasEF.Data
{
    public class SimchaContext : DbContext
    {
        private string _connectionString { get; set; }

        public SimchaContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Simcha> Simchas { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Deposit> Deposits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Contribution>()
                .HasKey(c => new { c.SimchaId, c.ContributorId });

            modelBuilder.Entity<Contribution>()
                .HasOne(c => c.Contributor)
                .WithMany(c => c.Contributions)
                .HasForeignKey(c => c.ContributorId);

            modelBuilder.Entity<Contribution>()
                .HasOne(c => c.Simcha)
                .WithMany(c => c.Contributions)
                .HasForeignKey(c => c.SimchaId);
        }
    }

}
