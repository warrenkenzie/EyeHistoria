using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class FamilyHealthHistory
    {
        public int p_id {  get; set; }

        [Display(Name = "Does the patient's family a history of health condition?")]
        public string pf_hdiseases { get; set; }

        [Display(Name = "Health Condition:")]
        public string hdis_type { get; set; }

        [Display(Name = "Family Member:")]
        public string hdis_member { get; set; }
    }
}
