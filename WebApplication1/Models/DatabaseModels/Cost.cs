using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class Cost
    {
        public int Id { get; set; }

        [Display(Name = "Rodzaj kosztu")]
        public int IdCostType { get; set; }

        [Required]
        [Display(Name = "Wartość")]
        public int Amount { get; set; }

        [Display(Name = "Kampania")]
        public int IdCampaign { get; set; }

        [Display(Name = "Kontrahent")]
        public int IdContractor { get; set; }

        [Display(Name = "Kampania")]
        public virtual Campaign IdCampaignNavigation { get; set; }

        [Display(Name = "Kontrahent")]
        public virtual Contractor IdContractorNavigation { get; set; }

        [Display(Name = "Rodzaj kosztu")]
        public virtual CostType IdCostTypeNavigation { get; set; }
    }
}
