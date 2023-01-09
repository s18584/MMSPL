#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class CustomerSendingAction
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public int IdSendingAction { get; set; }
        public int IsSend { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual SendingAction IdSendingActionNavigation { get; set; }
    }
}
