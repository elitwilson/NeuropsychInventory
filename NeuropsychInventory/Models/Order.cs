using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuropsychInventory.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual IList<OrderItem> OrderItems { get; set; }

        //Consider zero or one to one relationship with Inventory??
    }
}