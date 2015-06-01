using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notus.Portal.Models
{
    public class CalenderEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [DefaultValue(false)]
        public bool AllDay { get; set; }

        [DefaultValue("info")]
        public string ClassName { get; set; }

        public string Url { get; set; }
    }
}