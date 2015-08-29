using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class GroupUserConfiguration : EntityTypeConfiguration<GroupUser>
    {
        public GroupUserConfiguration()
        {
            Property(g => g.GroupId).IsRequired();
            Property(g => g.UserId).IsRequired().IsMaxLength();
            Property(g => g.Admin).IsRequired();
            Property(g => g.AddedDate).IsRequired();
        }
    }
}