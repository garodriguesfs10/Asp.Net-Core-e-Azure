using DevCompanyRating.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevCompanyRating.API.Persistence
{
    public class DevCompanyRatingDbContext : DbContext
    {
        public DevCompanyRatingDbContext(DbContextOptions<DevCompanyRatingDbContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyRating> CompanyRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Ratings)
                .WithOne()
                .HasForeignKey(r => r.IdCompany);

            modelBuilder.Entity<CompanyRating>()
                .HasKey(c => c.Id);
        }
    }
}
