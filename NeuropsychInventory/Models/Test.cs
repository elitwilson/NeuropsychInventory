using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NeuropsychInventory.Models
{
    public class Test
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}