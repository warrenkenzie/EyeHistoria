using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class PersonalOcularHistory
    {
        public int p_id { get; set; }

        [Display(Name = "Has the patient been prescribed eyewear?")]
        public string p_prescription { get; set; }

        [Display(Name = "Has patient underwent any eye surgery/procedure?")]
        public string p_procedure { get; set; }

        [Display(Name = "Does patient suffer from any eye condition?")]
        public string p_condition { get; set; }

        [Display(Name = "Eyewear:")]
        public string? pre_type { get; set; }

        [Display(Name = "Eye Surgery/Procedure:")]
        public string? pro_type { get; set; }

        [Display(Name = "Eye Condition:")]
        public string? con_type { get; set; }

        [Display(Name = "Date of Prescription:")]
        public DateTime? pre_sdate { get; set; }

        [Display(Name = "Date of Surgery/Procedure:")]
        public DateTime? pro_sdate { get; set; }

        [Display(Name = "Date of Diagnosis:")]
        public DateTime? con_sdate { get; set; }

        [Display(Name = "Has patient stop their eyewear usage?")]
        public DateTime? pre_edate { get; set; }

        [Display(Name = "Has patient fully recovered from said eye surgery/procedure?")]
        public DateTime? pro_edate { get; set; }

        [Display(Name = "Has the patient fully recovered from said eye condition?")]
        public DateTime? con_edate { get; set; }
    }
}
