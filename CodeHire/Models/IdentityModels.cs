using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeHire.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //public List<JobListing> JobListings { get; } = new();
        public List<JobListingApplicationUser> JobListingApplicationUsers { get; } = new();

        public Resume? Resume { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<Resume> Resumes { get; set; }

        public DbSet<JobHistory> JobHistories { get; set; }

        public DbSet<JobListingApplicationUser> JobListingApplicationUsers { get; set; }

        public DbSet<Skill> Skills { get; set; }

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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobListing>().HasMany(x => x.JobListingApplicationUsers).WithRequired(x => x.JobListing).HasForeignKey(x => x.JobListing_Id);
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.JobListingApplicationUsers).WithRequired(x => x.ApplicationUser).HasForeignKey(x => x.ApplicationUser_Id);
            modelBuilder.Entity<JobListingApplicationUser>().HasKey(x => new { x.ApplicationUser_Id, x.JobListing_Id });
        }
    }
}