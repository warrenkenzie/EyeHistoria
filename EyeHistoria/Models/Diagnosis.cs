using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class Diagnosis
    {
        [Display(Name = "ID")]
        public int DiagnosisID { get; set; }

        [Display(Name = "Diagnosis")]
        public string DiagnosisName { get; set; }

        [Display(Name = "Reference Link")]
        public string LearnMore { get; set; }

        [Display(Name = "Symptoms")]
        public List<string> List_of_diagnosis_symptoms { get; set;}

        [Display(Name = "Possible Tests")]
        public string Tests { get; set; }

        [Display(Name = "Admin ID")]
        public int AdminID { get; set; }

        [Display(Name = "Created By")]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set;}

        public List<Diagnosis_symptoms> List_diagnosis_symptoms { get; set;}
    }
}
