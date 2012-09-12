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
    }
}