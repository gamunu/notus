using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class GroupCommentUserConfguration : EntityTypeConfiguration<GroupCommentUser>
    {
        public GroupCommentUserConfguration()
        {
            Property(g => g.GroupCommentId).IsRequired();
            Property(g => g.UserId).IsRequired();
        }
    }
}