using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Notus.Portal.Models
{
    public class CalenderEvent
    {
        [Key]
        public int Id { get; set; }

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