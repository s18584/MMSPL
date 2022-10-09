using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class Promocode
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public int IdCampaign { get; set; }

        public virtual Campaign IdCampaignNavigation { get; set; }
    }
}
