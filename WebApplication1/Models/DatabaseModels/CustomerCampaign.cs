using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class CustomerCampaign
    {
        public int IdCustomer { get; set; }
        public int IdCampaign { get; set; }

        [Range(0, 1)]
        [Display(Name = "OK to EMAIL")]
        public int OkToEmail { get; set; }

        [Range(0, 1)]
        [Display(Name = "OK to 3RD")]
        public int OkToThirdParty { get; set; }

        [Display(Name = "OK to EMAIL")]
        public string OkToEmailText
        {
            get { return OkToEmail == 1 ? "TAK" : "NIE"; }
        }

        [Display(Name = "OK to 3RD")]
        public string OkToThirdPartyText
        {
            get { return OkToThirdParty == 1 ? "TAK" : "NIE"; }
        }

        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual Customer IdCustomerNavigation { get; set; }
    }
}
