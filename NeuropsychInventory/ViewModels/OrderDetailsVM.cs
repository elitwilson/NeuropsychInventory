using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeuropsychInventory.Models;

namespace NeuropsychInventory.ViewModels
{
    public class OrderDetailsVM
    {
        public Order Order { get; set; }
        public IList<ProductByTest> ProductsByTest { get; set; }

        public class ProductByTest
        {
            public int? TestId { get; set; }
            public string TestName { get; set; }
            public string ProductName { get; set; }
            public string ProductNumber { get; set; }
            public int Quantity { get; set; }
            public decimal Cost { get; set; }
        }

        public OrderDetailsVM()
        {
            ProductsByTest = new List<ProductByTest>();
        }
    }
}