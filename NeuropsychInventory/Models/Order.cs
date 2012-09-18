using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NeuropsychInventory.Models
{
    public class Order
    {
        public int Id { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual IList<OrderItem> OrderItems { get; set; }

        //Consider zero or one to one relationship with Inventory??
    }
}