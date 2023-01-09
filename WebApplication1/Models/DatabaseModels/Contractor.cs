using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class Contractor
    {
        public Contractor()
        {
            Campaigns = new HashSet<Campaign>();
            Costs = new HashSet<Cost>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Nazwa kontrahenta")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "Niepoprawny NIP")]
        [Display(Name = "NIP")]
        public string Nip { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
    }
}
