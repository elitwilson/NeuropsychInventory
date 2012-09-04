using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NeuropsychInventory.Models
{
    public class InventoryInitializer : DropCreateDatabaseAlways<InventoryContext>
    {
        protected override void Seed(InventoryContext context)
        {
            var companies = new List<Company>
            {
                new Company { Id=1, Name="Pearson" },
                new Company { Id=2, Name="PAR" },
                new Company { Id=3, Name="Riverside Publishing" }
            };
            companies.ForEach(c => context.Companies.Add(c));

            var tests = new List<Test>
            {
                new Test { Id=1, CompanyId = 1, Name = "Wechsler Adult Intelligence Scale -IV", Abbreviation = "WAIS-IV" },
                new Test { Id=2, CompanyId = 1, Name = "Wechsler Memory Scale - IV", Abbreviation = "WMS-IV" },
            };
            tests.ForEach(t => context.Tests.Add(t));

            var products = new List<Product>
            {
                //WAIS-IV Products
                new Product { Id=1, TestId=1, Name="Kit", PricePerUnit=40.99m, ItemsPerUnit=1, MaxInStock=null, UnitsInStock=10, Description="Complete Kit", ProductNumber="015-8980-808" },
                new Product { Id=2, TestId=1, Name="Record Forms", ItemsPerUnit=25, MaxInStock=50, UnitsInStock=41, Description="50 forms per package", ProductNumber="015-8980-905" },
                new Product { Id=3, TestId=2, Name="Adult Record Forms", ItemsPerUnit=25, MaxInStock=50, UnitsInStock=12, Description="50 forms per package", ProductNumber="015-8895-851" }
            };
            products.ForEach(p => context.Products.Add(p));

            var orders = new List<Order>
            {
                new Order { Id=1, Date = DateTime.Parse("2012-8-20") }, 
                new Order { Id=2, Date = DateTime.Parse("2012-5-12") }
            };
            orders.ForEach(o => context.Orders.Add(o));

            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId=1, ProductId=1, Quantity=1 }, 
                new OrderItem { OrderId=1, ProductId=2, Quantity=30 }, 
                new OrderItem { OrderId=1, ProductId=3, Quantity=20 }
            };
            orderItems.ForEach(o => context.OrderItems.Add(o));

            var inventory = new List<Inventory>
            {
                new Inventory { Id=1, Date=DateTime.Parse("2012-5-20") },
                new Inventory { Id=2, Date=DateTime.Parse("2012-8-11") }
            };
            inventory.ForEach(i => context.Inventory.Add(i));

            var inventoryItem = new List<InventoryItem>
            {
                new InventoryItem { InventoryId=1, ProductId=1, Quantity=17 },
                new InventoryItem { InventoryId=1, ProductId=2, Quantity=25 },
                new InventoryItem { InventoryId=1, ProductId=3, Quantity=30 }
            };
            inventoryItem.ForEach(i => context.InventoryItems.Add(i));
            
            context.SaveChanges();
        }
    }
}