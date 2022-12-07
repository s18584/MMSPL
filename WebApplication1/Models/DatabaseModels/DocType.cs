using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class DocType
    {
        public DocType()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }

        [Display(Name = "Typ dokumentu")]
        public string Name { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
