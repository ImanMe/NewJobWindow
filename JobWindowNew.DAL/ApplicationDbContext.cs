using JobWindowNew.Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace JobWindowNew.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<SalaryType> SalaryTypes { get; set; }
        public DbSet<JobBoard> JobBoards { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobOccupationMap> JobOccupationMaps { get; set; }
        public DbSet<JobCategoryMap> JobCategoryMaps { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                    .HasRequired(c => c.State)
                    .WithMany()
                    .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}