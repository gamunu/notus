using System.ComponentModel.DataAnnotations;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class CommentFormModel
    {
        [Required(ErrorMessage = "Required")]
        public string CommentText { get; set; }

        public int UpdateId { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}