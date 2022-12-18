using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.models.databasemodels;

namespace WebApplication1.Models.DTO
{
    public class CampaignDetailsDto
    {
        public CampaignDetailsDto()
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

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Budżet")]
        public int Budget { get; set; }

        
        public int UsersCountData { get; set; }
        public int CostSumData { get; set; }

        
        public int costUsagePercent { 
            get {
                return (int)Math.Round((double)(100 * CostSumData) / Budget);
            } 
        }

        [Display(Name = "Kontrahent")]
        public virtual Contractor IdContractorNavigation { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
        public virtual ICollection<CustomerCampaign> CustomerCampaigns { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Promocode> Promocodes { get; set; }
        public virtual ICollection<SendingAction> SendingActions { get; set; }
    }
}
