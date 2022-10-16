using System;
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

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }
        public int IdCampaign { get; set; }

        /*
        [Display(Name = "Tytuł emaila")]
        public string EmailSubject { get; set; }

        [Display(Name = "Treść emaila")]
        public string EmailBody { get; set; }
        */

        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual SendingActionType IdSendingActionTypeNavigation { get; set; }
        public virtual ICollection<CustomerSendingAction> CustomerSendingActions { get; set; }
    }
}
