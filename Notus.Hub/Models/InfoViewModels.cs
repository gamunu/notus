using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.Configuration;
using System.Web;

namespace Notus.Hub.Models
{
    public class BloodReportViewModel
    {
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        [DisplayName("White Blood Cell")]
        public double Wbc { get; set; }

        [DisplayName("Netutrophils")]
        public double Neut { get; set; }

        [DisplayName("Lymph")]
        public double Lymph { get; set; }

        [DisplayName("Mononucleosis")]
        public double Mono { get; set; }

        [DisplayName("Eosinophilia")]
        public double Eo { get; set; }

        [DisplayName("Basophil")]
        public double Baso { get; set; }

        [DisplayName("Red blood cells")]
        public double Rbc { get; set; }

        [DisplayName("Hemoglobin")]
        public double Hgb { get; set; }

        [DisplayName("Hematocrit")]
        public double Hct { get; set; }

        [DisplayName("Mean Corpuscular Volume")]
        public double Mcv { get; set; }

        [DisplayName("Mean Corpuscular Hemoglobin")]
        public double Mch { get; set; }

        [DisplayName("Mean Corpuscular Hemoglobin Concentration")]
        public double Mchc { get; set; }

        [DisplayName("Red Cell Distribution Width")]
        public double Rdw { get; set; }

        [DisplayName("Platelet Count")]
        public double Plt { get; set; }

        [DisplayName("Report Date")]
        public DateTime ReportDate { get; set; }
    }
}