using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(c => c.Email).IsRequired().HasMaxLength(150);
            Property(c => c.FirstName).IsRequired().HasMaxLength(1);
            Property(c => c.LastName).HasMaxLength(100);
            Property(c => c.Email).HasMaxLength(250);
        }
    }
}