using NeuropsychInventory.Models;
using System;
using System.Collections.Generic;

namespace NeuropsychInventory.ViewModels
{
    public class OrderDetailsVM
    {
        public Order Order { get; set; }
        public IList<OrderDetailsVM.Company> Companies { get; set; }
        public IList<Product> ProductsByCompany { get; set; }

        public class Company
        {
            public int CompanyId { get; set; }
            public string CompanyName { get; set; }
        }

        public class Product
        {
            public int? TestId { get; set; }
            public string TestName { get; set; }
            public int OrderId { get; set; }
            public int CompanyId { get; set; }
            public string CompanyName { get; set; }
            public string ProductName { get; set; }
            public string ProductNumber { get; set; }
            public int Quantity { get; set; }
            public Decimal PricePerUnit { get; set; }
        }

        public OrderDetailsVM()
        {
            ProductsByCompany = new List<Product>();
        }
    }
}
