using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class Document
    {
        public int Id { get; set; }
        public int IdDocType { get; set; }

        [Display(Name = "Ścieżka")]
        public string Path { get; set; }
        public int IdCampaign { get; set; }

        public virtual DocType IdDocTypeNavigation { get; set; }
        public virtual Campaign IdCampaignNavigation { get; set; }
    }
}
