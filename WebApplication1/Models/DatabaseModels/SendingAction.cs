using System;
using System.Collections.Generic;

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
        public int IdSendingActionType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdCampaign { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
        public virtual SendingActionType IdSendingActionTypeNavigation { get; set; }
        public virtual ICollection<CustomerSendingAction> CustomerSendingActions { get; set; }
    }
}
