using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class Contractor
    {
        public Contractor()
        {
            Campaigns = new HashSet<Campaign>();
            Costs = new HashSet<Cost>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
    }
}
