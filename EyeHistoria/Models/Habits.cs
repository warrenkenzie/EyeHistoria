using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class Habits
    {
        public int p_id {  get; set; }

        [Required(ErrorMessage = "Smoking is required.")]
        [Display(Name = "Smoking")]
        public string p_smoke { get; set; }

        [Required(ErrorMessage = "Drinking is required.")]
        [Display(Name = "Drinking")]
        public string p_drink { get; set; }

        [Display(Name = "Smoking Frequency")]
        public int? s_freq { get; set; }

        [Display(Name = "Drinking Frequency")]
        public int? d_freq {  get; set; }

        [Required(ErrorMessage = "Gaming is required.")]
        [Display(Name = "Gaming")]
        public string p_game { get; set; }

        [Required(ErrorMessage = "Reading is required.")]
        [Display(Name = "Reading")]
        public string p_read { get; set; }

        [Display(Name = "Gaming Frequency")]
        public int g_freq { get; set; }

        [Display(Name = "Reading Frequency")]
        public int r_freq { get; set; }
    }
}
