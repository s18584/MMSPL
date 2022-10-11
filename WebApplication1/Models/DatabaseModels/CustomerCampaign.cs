using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class CustomerCampaign
    {
        public int IdCustomer { get; set; }
        public int IdCampaign { get; set; }

        [Display(Name = "OK to EMAIL")]
        public int OkToEmail { get; set; }

        [Display(Name = "OK to 3RD")]
        public int OkToThirdParty { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual Customer IdCustomerNavigation { get; set; }
    }
}
