using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class GroupRequestConfiguration : EntityTypeConfiguration<GroupRequest>
    {
        public GroupRequestConfiguration()
        {
            Property(g => g.GroupId).IsRequired();
            Property(g => g.UserId).HasMaxLength(128);
            Property(g => g.Accepted).IsRequired();
        }
    }
}