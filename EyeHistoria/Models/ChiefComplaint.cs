using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class ChiefComplaint
    {
        public int p_id { get; set; }

        [Display(Name = "Purpose of patient's visit:")]
        public string p_chiefcomplaint { get; set; }
    }
}
