using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class GroupUpdateSupportConfiguration : EntityTypeConfiguration<GroupUpdateSupport>
    {
        public GroupUpdateSupportConfiguration()
        {
            Property(g => g.GroupUpdateId).IsRequired();
            Property(g => g.GroupUserId).IsRequired();
            Property(g => g.UpdateSupportedDate).IsRequired();
        }
    }
}