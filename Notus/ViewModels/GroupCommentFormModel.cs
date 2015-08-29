using System.ComponentModel.DataAnnotations;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class GroupCommentFormModel
    {
        [Required(ErrorMessage = "*")]
        public string CommentText { get; set; }

        public int GroupUpdateId { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}