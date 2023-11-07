using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class AddDiagnosisViewModel
    {
        [Display(Name = "ID")]
        public int DiagnosisID { get; set; }

        [Display(Name = "Diagnosis")]
        public string DiagnosisName { get; set; }

        [Display(Name = "Symptoms")]
        public List<Boolean> List_of_diagnosis_symptoms_checkbox { get; set; }

        [Display(Name = "Admin ID")]
        public int AdminID { get; set; }

        [Display(Name = "Created By")]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
