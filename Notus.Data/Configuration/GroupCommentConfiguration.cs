using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class GroupCommentConfiguration : EntityTypeConfiguration<GroupComment>
    {
        public GroupCommentConfiguration()
        {
            Property(g => g.GroupUpdateId).IsRequired();
            Property(g => g.CommentText).IsMaxLength();
            Property(g => g.CommentDate).IsRequired();
        }
    }
}