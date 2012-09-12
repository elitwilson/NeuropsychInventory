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

            Order order = db.Orders
                .Select(x => x)
                .ToList()
                .LastOrDefault();

            /*Need to use an inner join for InventoryItems and Products
             * Typical format for inner join is as follows:
             * 
             * from table1 in db.Table1
             * join table2 in db.Table2 on table1.field equals table2.field
             * select table1
             */            
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

            return View(vm);
            
        }

        //
        // GET: /Order/Details/5

        public ActionResult Details(int id)
        {
            return View();
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
                OrderItem orderItem = new OrderItem();
                orderItem.ProductId = item.ProductId;
                orderItem.Quantity = item.Product.MaxInStock - item.Product.UnitsInStock;
                orderItem.Product = item.Product;
                orderItem.Order = order;
                orderItem.OrderId = 0;
                order.OrderItems.Add(orderItem);
                
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
            orderInput.Date = DateTime.Now;
            var inventory = from item in db.Inventories
                            where item.Id == 1
                            select item;

            db.Orders.Add(orderInput);
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
