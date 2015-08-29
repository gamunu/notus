using System;
using System.ComponentModel.DataAnnotations;

namespace Notus.ViewModels
{
    public class GroupFormModel
    {
        public GroupFormModel()
        {
            CreatedDate = DateTime.Now;
        }

        public int GroupId { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50)]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }
    }
}