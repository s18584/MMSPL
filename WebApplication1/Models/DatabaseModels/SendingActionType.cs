using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class SendingActionType
    {
        public SendingActionType()
        {
            SendingActions = new HashSet<SendingAction>();
        }

        public int Id { get; set; }

        [Display(Name = "Typ wysyłki")]
        public string Name { get; set; }

        public virtual ICollection<SendingAction> SendingActions { get; set; }
    }
}
