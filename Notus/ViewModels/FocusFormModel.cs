using System;
using System.ComponentModel.DataAnnotations;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class FocusFormModel
    {
        public FocusFormModel()
        {
            CreatedDate = DateTime.Now;
        }

        public int FocusId { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50)]
        public string FocusName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100)]
        public string Description { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}