using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            Property(g => g.GroupName).HasMaxLength(50);
            Property(g => g.CreatedDate).IsRequired();
            Property(g => g.Description).IsMaxLength();
        }
    }
}