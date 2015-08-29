using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class UpdateSupportConfiguration : EntityTypeConfiguration<UpdateSupport>
    {
        public UpdateSupportConfiguration()
        {
            Property(u => u.UpdateId).IsRequired();
            Property(u => u.UserId).IsMaxLength();
            Property(u => u.UpdateSupportedDate).IsRequired();
        }
    }
}