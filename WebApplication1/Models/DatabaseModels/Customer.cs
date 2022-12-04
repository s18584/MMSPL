using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Service;

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

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Data urodzenia")]
        [DisplayFormat(DataFormatString ="{0:d}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Wiek")]
        public int Age
        {
            get { return DateTime.Today.Year - BirthDate.Year; }
        }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Pełne imię")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Display(Name = "Województwo")]
        public string Area
        {
            get { return PostCodeService.GetAreaFromPostCode(PostCode); }
        }
        public virtual ICollection<CustomerCampaign> CustomerCampaigns { get; set; }
        public virtual ICollection<CustomerSendingAction> CustomerSendingActions { get; set; }
    }
}
