using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuropsychInventory.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Notes { get; set; }
        public string ContractNumber { get; set; }
        public string VendorNumber { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}