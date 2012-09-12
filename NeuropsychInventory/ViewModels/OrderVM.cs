using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeuropsychInventory.Models;

namespace NeuropsychInventory.ViewModels
{
    public class OrderVM
    {
        public Inventory Inventory { get; set; }
        public Order Order { get; set; }
    }
    public class AutoOrderVM
    {
        public int OrderId { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class ManualOrderVM
    {
    }
}