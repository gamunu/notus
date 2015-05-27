using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Notus.Portal.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

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
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        public DbSet<CalenderEvent> CalenderEvents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Cause> Causes { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");

            // Change the name of the table to be UserRoles instead of AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");

            // Change the name of the table to be UserLogins instead of AspNetUserLogins
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");

            // Change the name of the table to be UserClaims instead of AspNetUserClaims
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            // Change the name of the table to be Roles instead of AspNetRoles
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}