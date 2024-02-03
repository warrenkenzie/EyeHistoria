using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class FamilyOcularHistory
    {
        public int p_id { get; set; }

        [Display(Name = "Does the patient's family have a history of eye disease?")]
        public string pf_diseases { get; set; }

        [Display(Name = "Eye Disease:")]
        public string? dis_type { get; set; }

        [Display(Name = "Family Member:")]
        public string? dis_member { get; set; }
    }
}
