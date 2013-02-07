using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using NeuropsychInventory.Models;
using NeuropsychInventory.ViewModels;

namespace NeuropsychInventory.Controllers
{
    public class OrderController : Controller
    {
        private InventoryContext db = new InventoryContext();
        
        //
        // GET: /Order/

        public ActionResult Index()
        {
            OrderVM vm = new OrderVM();

            Inventory inventory = db.Inventories
                .Select(x => x)
                .ToList()
                .LastOrDefault();

            vm.Orders = db.Orders.Select(x => x)
                .OrderByDescending(x => x.Date);

            vm.Inventory = inventory;

            return View(vm);

            /* Code kept for historical significance, and informative of conducting 
             * inner joins:
             * 
             * Need to use an inner join for InventoryItems and Products
             * Typical format for inner join is as follows:
             * 
             * from table1 in db.Table1
             * join table2 in db.Table2 on table1.field equals table2.field
             * select table1
                         
            inventory.InventoryItems = (
                from invItem in inventory.InventoryItems
                join product in db.Products on invItem.ProductId equals product.Id
                select invItem).OrderBy(x => x.Product.Test.Abbreviation).ToList();

            order.OrderItems = (
                from ordItem in order.OrderItems
                join product in db.Products on ordItem.ProductId equals product.Id
                select ordItem).OrderBy(x => x.Product.Test.Abbreviation).ToList();
            
            vm.Inventory = inventory;
            vm.Order = order;
            */
            
        }

        //
        // GET: /Order/Details/5

        public ActionResult Details(int id)
        {
            OrderDetailsVM vm = new OrderDetailsVM();

            var companiesFromOrder = (from c in db.Companies
                                      join o in db.OrderItems on c.Id equals o.Product.Test.CompanyId
                                      where o.OrderId == id
                                      select c)
                                      .Distinct()
                                      .OrderBy(x => x.Name)
                                      .ToList();

            foreach (var c in companiesFromOrder) {
                OrderDetailsVM.Company company = new OrderDetailsVM.Company {
                    CompanyId = c.Id,
                    CompanyName = c.Name
                };
                vm.Companies.Add(company);
            }

            vm.Order = db.Orders.Find(id);
            foreach (var item in vm.Order.OrderItems) {
                OrderDetailsVM.Product product = new OrderDetailsVM.Product {
                    TestId = item.Product.TestId,
                    TestName = item.Product.Test.Abbreviation,
                    CompanyId = item.Product.Test.Company.Id,
                    CompanyName = item.Product.Test.Company.Name,
                    ProductName = item.Product.Name,
                    ProductNumber = item.Product.ProductNumber,
                    Quantity = item.Quantity,
                    PricePerUnit = item.Product.PricePerUnit
                };
                vm.ProductsByOrder.Add(product);
            }

            //LINQ Query used here simply to sort results. Could write an extension method
            //to add this functionality to IList (saw a blog post on it)
            vm.ProductsByOrder = (from p in vm.ProductsByOrder
                                  select p)
                                  .OrderBy(x => x.TestName)
                                  .ThenBy(x => x.ProductName)
                                  .ToList();
            return View(vm);
        }

        //
        // GET: /Order/AutoOrder

        public ActionResult AutoOrder()
        {
            //AutoOrderVM vm = new AutoOrderVM();
            Order order = new Order();
            order.OrderItems = new List<OrderItem>();
            Inventory inventory = db.Inventories
                .Select(x => x)
                .ToList()
                .LastOrDefault();

            inventory.InventoryItems = (
                from invItem in inventory.InventoryItems
                join product in db.Products on invItem.ProductId equals product.Id
                select invItem).OrderBy(x => x.Product.Test.Abbreviation).ToList();

            foreach (var item in inventory.InventoryItems)
            {
                OrderItem orderItem = new OrderItem {
                    ProductId = item.ProductId,
                    Quantity = item.Product.MaxInStock - item.Product.UnitsInStock,
                    Product = item.Product,
                    Order = order,
                    OrderId = 0
                };
                if (orderItem.Quantity > 0) {
                    order.OrderItems.Add(orderItem);
                }
                
                
            }

            foreach (var item in order.OrderItems)
            {
                order.TotalPrice = order.TotalPrice + item.Product.PricePerUnit * item.Quantity;
            }

            return View(order);
        }

        //
        // POST: /Order/AutoOrder
        [HttpPost]
        public ActionResult AutoOrder(Order orderInput)
        {
            //orderInput.Date = DateTime.Now;
            //var inventory = from item in db.Inventories
            //                where item.Id == 1
            //                select item;

            //db.Orders.Add(orderInput);
            List<OrderItem> orderItems = new List<OrderItem>();
            Order order = new Order();

            foreach (var item in orderInput.OrderItems) {
                if (item.Quantity > 0) {
                    orderItems.Add(item);
                }
            }
            order.Date = DateTime.Now;
            order.OrderItems = orderItems;
            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Order/ManualOrder

        public ActionResult ManualOrde()
        {
            return View();
        }

        //
        // GET: /Order/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Order/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Order/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Order/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
