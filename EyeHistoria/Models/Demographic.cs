using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class Demographic
    {
        public int p_id { get; set; }

        [Display(Name = "Name:")]
        public string p_name { get; set; }

        [Display(Name = "Race:")]
        public string p_race { get; set; }

        [Display(Name = "Age:")]
        public int p_age { get; set; }

        [Display(Name = "Occupation:")]
        public string p_occupation { get; set; }

        [Display(Name = "Gender:")]
        public string p_gender { get; set; }
    }
}
