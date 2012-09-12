using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeuropsychInventory.Models;

namespace NeuropsychInventory.ViewModels
{
    public class TakeInventoryVM
    {
        public int InventoryId { get; set; }
        public DateTime Date { get; set; }
        public string TestName { get; set; }
        public string ProductName { get; set; }


        public IEnumerable<Test> Tests { get; set; }
        public IEnumerable<InventoryItem> InventoryItems { get; set; }

        //For index.cshtml
        public IEnumerable<Inventory> Inventories { get; set; }
        public bool AllInventoriesComplete { get; set; }
    }
}