using Microsoft.AspNetCore.Mvc;
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

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres email")]
        [Remote(action: "VerifyEmail", controller: "Customers")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Data urodzenia")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Wiek")]
        public int Age
        {
            get { return DateTime.Today.Year - BirthDate.Year; }
        }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(50)]
        [RegularExpression(@"^[0-9]{2}-[0-9]{3}$",
            ErrorMessage = "Niepoprawny format")]
        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Pełne imię")]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        [Display(Name = "Pełny adres")]
        public string FullAddress
        {
            get { return $"{Address}, {PostCode} {City}"; }
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
