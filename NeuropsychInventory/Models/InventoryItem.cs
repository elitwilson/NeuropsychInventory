using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeuropsychInventory.Models
{
    public class InventoryItem
    {
        public int ProductId { get; set; }
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public bool IsInventoried { get; set; }

        public Product Product { get; set; }
        public Inventory Inventory { get; set; }

    }
}