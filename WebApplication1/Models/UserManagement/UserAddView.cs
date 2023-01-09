using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.UserManagement
{
    public class UserAddView
    {
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string Role { get; set; }

    }
}
