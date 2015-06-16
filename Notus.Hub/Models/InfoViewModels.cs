using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Notus.Hub.Models
{
    public class BloodTestViewModel
    {
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }


    }
}