using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class PersonalHealthHistory
    {
        [Display(Name = "Patient ID")]
        public int p_id {  get; set; }

        [Required(ErrorMessage = "Allergy is required.")]
        [Display(Name = "Allergy")]
        public string p_allergy { get; set; }

        [Required(ErrorMessage = "Medication is required.")]
        [Display(Name = "Patient Medication")]
        public string p_medication { get; set; }

        [Required(ErrorMessage = "Health Condition is required.")]
        [Display(Name = "Patient Health Condition")]
        public string p_hcondition { get; set; }

        public string all_type { get; set; }

        public string med_type { get; set; }

        public string hcon_type { get; set; }

        public DateTime med_sdate { get; set; }

        public DateTime hcon_sdate { get; set; }

        public DateTime med_edate { get; set; }

        public DateTime hcon_edate { get; set; }
    }
}
