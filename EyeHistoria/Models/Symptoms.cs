using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace EyeHistoria.Models
{
    public class Symptoms
    {
        [Display(Name =  "ID")]
        public int SymptomID { get; set; }

        [Required(ErrorMessage = "Oops! You must enter a symptom!")]
        [ValidateSymptomExists]
        [Display(Name = "Symptom")]
        public string SymptomName { get; set; }

        [Display(Name = "Admin ID")]
        public int AdminID { get; set; }

        [Display(Name = "Created By")]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
