using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication1.models.databasemodels;

namespace WebApplication1.Models.DTO
{
    public class AddFileModel
    {

        public int Id { get; set; }
        public int IdDocType { get; set; }
        public IFormFile Path { get; set; }
        public int IdCampaign { get; set; }
        
        public string Description { get; set; }

        public virtual DocType IdDocTypeNavigation { get; set; }
        public virtual Campaign IdCampaignNavigation { get; set; }

    }
}
