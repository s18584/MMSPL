using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class SendingAction
    {
        public SendingAction()
        {
            CustomerSendingActions = new HashSet<CustomerSendingAction>();
        }

        public int Id { get; set; }

        [Display(Name = "Typ wysyłki")]
        public int IdSendingActionType { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        public int IdCampaign { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Tytuł emaila")]
        public string EmailSubject { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Treść emaila")]
        public string EmailBody { get; set; }

        [Display(Name = "Kampania")]
        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual SendingActionType IdSendingActionTypeNavigation { get; set; }
        public virtual ICollection<CustomerSendingAction> CustomerSendingActions { get; set; }
    }
}
