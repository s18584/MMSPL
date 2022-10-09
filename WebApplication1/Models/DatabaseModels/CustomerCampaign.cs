using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class CustomerCampaign
    {
        public int IdCustomer { get; set; }
        public int IdCampaign { get; set; }
        public int OkToEmail { get; set; }
        public int OkToThirdParty { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual Customer IdCustomerNavigation { get; set; }
    }
}
