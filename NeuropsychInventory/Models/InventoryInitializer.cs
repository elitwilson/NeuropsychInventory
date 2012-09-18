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
                new Test { Id=3, CompanyId = 2, Name = "Wisconsin Card Sorting Test", Abbreviation="WCST" },
                new Test { Id=4, CompanyId = 2, Name = "Gordon Diagnostic System", Abbreviation = "Gordon" }
            };
            tests.ForEach(t => context.Tests.Add(t));

            var products = new List<Product>
            {
                //WAIS-IV Products
                new Product { Id=1, TestId=1, RegularlyOrdered=false, Name="Kit", PricePerUnit=40.99m, ItemsPerUnit=1, MaxInStock=10, UnitsInStock=10, Description="Complete Kit", ProductNumber="015-8980-808" },
                new Product { Id=2, TestId=1, RegularlyOrdered=true,  Name="Record Forms", PricePerUnit=25.99m, ItemsPerUnit=25, MaxInStock=50, UnitsInStock=41, Description="50 forms per package", ProductNumber="015-8980-905" },
                //WMS-IV Products
                new Product { Id=3, TestId=2, RegularlyOrdered=true, Name="Adult Record Forms", PricePerUnit=30.99m, ItemsPerUnit=25, MaxInStock=50, UnitsInStock=12, Description="50 forms per package", ProductNumber="015-8895-851" },
                new Product { Id=4, TestId=2, RegularlyOrdered=true, Name="Older Adult Record Forms", PricePerUnit=30.59m, ItemsPerUnit=25, MaxInStock=50, UnitsInStock=12, Description="50 forms per package", ProductNumber="015-8895-255" },
                new Product { Id=5, TestId=2, RegularlyOrdered=false, Name="Kit", PricePerUnit=100.00m, ItemsPerUnit=1, MaxInStock=10, UnitsInStock=10, Description="Complete Kit", ProductNumber="235-2652-234" },
                new Product { Id=6, TestId=3, RegularlyOrdered=false, Name="Stimulus Cards", PricePerUnit=50.00m, ItemsPerUnit=1, MaxInStock=10, UnitsInStock=10, Description="Complete Kit", ProductNumber="112-3314-551" },
                new Product { Id=7, TestId=3, RegularlyOrdered=true, Name="Record Forms", PricePerUnit=100.00m, ItemsPerUnit=25, MaxInStock=50, UnitsInStock=10, Description="NA", ProductNumber="112-3314-783" },
                new Product { Id=8, TestId=3, RegularlyOrdered=false, Name="Manual", PricePerUnit=39.99m, ItemsPerUnit=1, MaxInStock=10, UnitsInStock=10, Description="NA", ProductNumber="112-3314-151" },
                new Product { Id=9, TestId=4, RegularlyOrdered=true, Name="Record Forms", PricePerUnit=39.99m, ItemsPerUnit=30, MaxInStock=50, UnitsInStock=10, Description="NA", ProductNumber="GD1451" }
            };
            products.ForEach(p => context.Products.Add(p));

            var orders = new List<Order>
            {
                new Order { Id=1, Date = DateTime.Parse("2012-5-12") }, 
                new Order { Id=2, Date = DateTime.Parse("2012-8-20") }
            };
            orders.ForEach(o => context.Orders.Add(o));

            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId=2, ProductId=2, Quantity=1 }, 
                new OrderItem { OrderId=2, ProductId=3, Quantity=30 }, 
                new OrderItem { OrderId=2, ProductId=4, Quantity=20 }
            };
            orderItems.ForEach(o => context.OrderItems.Add(o));

            var inventory = new List<Inventory>
            {
                new Inventory { Id=1, Date=DateTime.Parse("2012-5-20"), Completed=true, HasAssociatedOrder=true },
                new Inventory { Id=2, Date=DateTime.Parse("2012-8-11"), Completed=true, HasAssociatedOrder=false }
            };
            inventory.ForEach(i => context.Inventories.Add(i));

            var inventoryItem = new List<InventoryItem>
            {
                new InventoryItem { InventoryId=1, ProductId=1, Quantity=17 },
                new InventoryItem { InventoryId=1, ProductId=2, Quantity=25 },
                new InventoryItem { InventoryId=1, ProductId=3, Quantity=30 },
                new InventoryItem { InventoryId=2, ProductId=2, Quantity=20 },
                new InventoryItem { InventoryId=2, ProductId=1, Quantity=21 }, 
                new InventoryItem { InventoryId=2, ProductId=3, Quantity=10 }
            };
            inventoryItem.ForEach(i => context.InventoryItems.Add(i));
            
            context.SaveChanges();
        }
    }
}