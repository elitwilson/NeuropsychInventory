using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeuropsychInventory.Models;

namespace NeuropsychInventory.ViewModels
{
    public class InventoryVM
    {
        public DateTime Date { get; set; }
        public string TestName { get; set; }
        public string ProductName { get; set; }
        public int QuantityInInventory { get; set; }
        public int MaxInStock { get; set; }

        public IEnumerable<InventoryItem> InventoryItems { get; set; }
    }
}