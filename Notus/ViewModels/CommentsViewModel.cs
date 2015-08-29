using System;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class CommentsViewModel
    {
        public int CommentId { get; set; }

        public string CommentText { get; set; }

        public int UpdateId { get; set; }

        public string UserId { get; set; }

        public DateTime CommentDate { get; set; }

        public ApplicationUser User { get; set; }
    }
}