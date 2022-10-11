﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class Cost
    {
        public int Id { get; set; }
        public int IdCostType { get; set; }

        [Display(Name = "Wartość")]
        public int Amount { get; set; }

        public int IdCampaign { get; set; }
        public int IdContractor { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual Contractor IdContractorNavigation { get; set; }
        public virtual CostType IdCostTypeNavigation { get; set; }
    }
}
