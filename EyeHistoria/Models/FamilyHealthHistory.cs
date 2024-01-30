using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class FamilyHealthHistory
    {
        [Display(Name = "Patient ID")]
        public int p_id {  get; set; }

        [Required(ErrorMessage = "Family Health Disease is required.")]
        [Display(Name = "Patient Family Health Disease")]
        public string pf_hdiseases { get; set; }

        public string hdis_type { get; set; }

        public string hdis_member { get; set; }
    }
}
