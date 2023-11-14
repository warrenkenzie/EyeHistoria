using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class Admin
    {
        [Display(Name = "Admin ID")]
        public int AdninID { get; set; }

        [Display(Name = "Admin Name")]
        public string AdminName { get; set; }

        [Display(Name = "Login ID")]
        public string LoginID { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
