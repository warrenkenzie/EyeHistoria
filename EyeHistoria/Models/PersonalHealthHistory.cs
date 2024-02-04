using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class PersonalHealthHistory
    {
        public int p_id {  get; set; }

        [Display(Name = "Does the patient have an allergy?")]
        public string p_allergy { get; set; }

        [Display(Name = "Is the patient on any medication?")]
        public string p_medication { get; set; }

        [Display(Name = "Does the patient suffer from any health condition?")]
        public string p_hcondition { get; set; }

        [Display(Name = "Allergy:")]
        public string all_type { get; set; }

        [Display(Name = "Medication:")]
        public string med_type { get; set; }

        [Display(Name = "Health Condition:")]
        public string hcon_type { get; set; }

        [Display(Name = "Date of Presciption:")]
        public DateTime? med_sdate { get; set; }

        [Display(Name = "Date of Diagnosis:")]
        public DateTime? hcon_sdate { get; set; }

        [Display(Name = "Is the patient still on medication?")]
        public DateTime? med_edate { get; set; }

        [Display(Name = "Is the patient full recovered from said health condition?")]
        public DateTime? hcon_edate { get; set; }
    }
}
