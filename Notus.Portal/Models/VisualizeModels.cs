using System.ComponentModel.DataAnnotations;

namespace Notus.Portal.Models
{
    public class Cause
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int? ParentCause { get; set; }
    }
}