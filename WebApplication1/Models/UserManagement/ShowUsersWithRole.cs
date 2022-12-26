using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.UserManagement
{
    public class ShowUsersWithRole
    {
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }
        [Display(Name = "Data zablokowania")]
        public DateTimeOffset? LockoutEnd { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "Guid")]
        public string Id { get; set; }
        [Display(Name = "Rola")]
        public string Role { get; set; }
    }
}
