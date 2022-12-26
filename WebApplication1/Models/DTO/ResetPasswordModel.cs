using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication1.Models.DTO
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Email/Login")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Powtórz hasło")]
        public string RepeatedPassword { get; set; }

        public string ResetPasswordToken { get; set; }
    }
}