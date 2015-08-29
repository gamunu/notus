using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class CommentUserConfiguration : EntityTypeConfiguration<CommentUser>
    {
        public CommentUserConfiguration()
        {
            Property(c => c.CommentId).IsRequired();
            Property(c => c.UserId).IsRequired();
        }
    }
}