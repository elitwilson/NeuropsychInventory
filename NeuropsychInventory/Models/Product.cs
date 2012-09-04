using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NeuropsychInventory.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int? TestId { get; set; }

        [Display(Name = "Product")]
        public string Name { get; set; }

        [Display(Name="Unit Price")]
        [DataType(DataType.Currency)]
        public decimal? PricePerUnit { get; set; }

        [Display(Name="Items/Unit")]
        public int ItemsPerUnit { get; set; }
        
        [Display(Name="Product #")]
        public string ProductNumber { get; set; }
        public string Description { get; set; }
         
        [Display(Name="Ideal #")]
        public int? MaxInStock { get; set; }
           
        [Display(Name="Units In Stock")]
        public int UnitsInStock { get; set; }

        [Display(Name="Regularly Ordered")]
        public bool RegularlyOrdered { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}