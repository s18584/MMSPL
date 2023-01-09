using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class LoginViewModel
    {

        [EmailAddress]
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Pole e-mail jest wymagane")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Pole hasło jest wymagane")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
