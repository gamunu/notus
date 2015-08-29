using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class GroupUpdateUserConfiguration : EntityTypeConfiguration<GroupUpdateUser>
    {
        public GroupUpdateUserConfiguration()
        {
            Property(g => g.GroupUpdateUserId).IsRequired();
            Property(g => g.UserId).IsRequired();
        }
    }
}