using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class CostType
    {
        public CostType()
        {
            Costs = new HashSet<Cost>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Cost> Costs { get; set; }
    }
}
