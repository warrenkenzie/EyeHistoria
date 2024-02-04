using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class ChiefComplaint
    {
        public int p_id { get; set; }

        [Display(Name = "Reason for Patient's Visit:")]
        public string p_chiefcomplaint { get; set; }
    }
}
