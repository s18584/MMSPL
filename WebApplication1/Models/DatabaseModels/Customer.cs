using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerCampaigns = new HashSet<CustomerCampaign>();
            CustomerSendingActions = new HashSet<CustomerSendingAction>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }

        public virtual ICollection<CustomerCampaign> CustomerCampaigns { get; set; }
        public virtual ICollection<CustomerSendingAction> CustomerSendingActions { get; set; }
    }
}
