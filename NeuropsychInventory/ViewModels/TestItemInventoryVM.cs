using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeuropsychInventory.Models;

namespace NeuropsychInventory.ViewModels
{
    public class TestItemInventoryVM
    {
        public int InventoryId { get; set; }
        public Test Test { get; set; }
        public IList<Product> RegularlyOrderedProducts { get; set; }

        public class Product {
            public int Id { get; set; }
            public string Name { get; set; }
            public int MaxInStock { get; set; }
            public int UnitsInStock { get; set; }
            public bool IsInventoried { get; set; }
        }

        public TestItemInventoryVM() {
            RegularlyOrderedProducts = new List<Product>();
        }
    }
}