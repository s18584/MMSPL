using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class Document
    {
        public int Id { get; set; }
        [Display(Name = "Typ dokumentu")]
        public int IdDocType { get; set; }

        [Display(Name = "Ścieżka")]
        public string Path { get; set; }
        public int IdCampaign { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Typ dokumentu")]
        public virtual DocType IdDocTypeNavigation { get; set; }
        [Display(Name = "Kampania")]
        public virtual Campaign IdCampaignNavigation { get; set; }
    }
}
