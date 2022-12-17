using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antlr4.StringTemplate;
using WebApplication1.models.databasemodels;

namespace WebApplication1.Service
{
    public class CompilerTemplateService
    {
        public const char DelimiterChar = '$';

        public static string ApplyTemplate(string html, Customer customer)
        {
            var sTemplate = new Template(html, DelimiterChar, DelimiterChar);

            sTemplate.Add("FirstName", customer.FirstName);
            sTemplate.Add("LastName", customer.LastName);
            sTemplate.Add("Email", customer.Email);
            sTemplate.Add("Area", customer.Area);
            sTemplate.Add("BirthDate", customer.BirthDate);
            sTemplate.Add("Age", customer.Age);
            sTemplate.Add("Address", customer.Address);
            sTemplate.Add("PostCode", customer.PostCode);
            sTemplate.Add("City", customer.City);


            return sTemplate.Render();
        }

    }
}
