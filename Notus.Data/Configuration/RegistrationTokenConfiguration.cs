using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class RegistrationTokenConfiguration : EntityTypeConfiguration<RegistrationToken>
    {
        public RegistrationTokenConfiguration()
        {
            Property(r => r.Role).HasMaxLength(50);
        }
    }
}