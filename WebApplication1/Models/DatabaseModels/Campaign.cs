using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class Campaign
    {
        public Campaign()
        {
            Costs = new HashSet<Cost>();
            CustomerCampaigns = new HashSet<CustomerCampaign>();
            Documents = new HashSet<Document>();
            Promocodes = new HashSet<Promocode>();
            SendingActions = new HashSet<SendingAction>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Nazwa kampanii")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Kontrahent")]
        public int IdContractor { get; set; }
        [Display(Name = "Kontrahent")]
        public virtual Contractor IdContractorNavigation { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
        public virtual ICollection<CustomerCampaign> CustomerCampaigns { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Promocode> Promocodes { get; set; }
        public virtual ICollection<SendingAction> SendingActions { get; set; }
    }
}
