using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class Promocode
    {
        public int Id { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Wartość")]
        public string Value { get; set; }
        public int IdCampaign { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
    }
}
