using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class Habits
    {
        public int p_id {  get; set; }

        [Display(Name = "Does the patient smoke?")]
        public string p_smoke { get; set; }

        [Display(Name = "Does the patient drink?")]
        public string p_drink { get; set; }

        [Display(Name = "Cigars/per day:")]
        public int s_freq { get; set; }

        [Display(Name = "Drinks/per day:")]
        public int d_freq {  get; set; }

        [Display(Name = "Does the patient play video games?")]
        public string p_game { get; set; }

        [Display(Name = "Does the patient read?")]
        public string p_read { get; set; }

        [Display(Name = "Hours/per day:")]
        public int g_freq { get; set; }

        [Display(Name = "Hours/per day:")]
        public int r_freq { get; set; }
    }
}
