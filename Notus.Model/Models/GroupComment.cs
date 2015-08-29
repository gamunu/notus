using System;

namespace Notus.Model.Models
{
    public class GroupComment
    {
        public GroupComment()
        {
            CommentDate = DateTime.Now;
        }

        public int GroupCommentId { get; set; }

        public string CommentText { get; set; }

        public int GroupUpdateId { get; set; }

        public DateTime CommentDate { get; set; }

        public virtual GroupUpdate GroupUpdate { get; set; }
    }
}