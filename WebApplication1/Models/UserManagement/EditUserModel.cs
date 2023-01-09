using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.UserManagement
{
    public class EditUserModel
    {
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Email/Nazwa Użytkownika")]
        public string Email { get; set; }
        [Display(Name = "Guid używkownika")]
        public string Id { get; set; }
        [Display(Name = "Grupa uprawnień")]
        public string Role { get; set; }
    }
}
