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

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}