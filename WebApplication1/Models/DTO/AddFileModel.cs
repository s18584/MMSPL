using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApplication1.models.databasemodels;

namespace WebApplication1.Models.DTO
{
    public class AddFileModel
    {

        public int Id { get; set; }
        [Display(Name = "Typ dokumentu")]
        public int IdDocType { get; set; }

        [Display(Name = "Plik")]
        public IFormFile Path { get; set; }

        [Display(Name = "Kampania")]
        public int IdCampaign { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        public virtual DocType IdDocTypeNavigation { get; set; }
        public virtual Campaign IdCampaignNavigation { get; set; }

    }
}
