﻿using System;
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

            vm.Order = db.Orders.Find(id);

            //var productsByTest = (
            //    from product in db.Products
            //    join test in db.Tests on product.TestId equals test.Id
            //    select product).OrderBy(x => x.Test.Abbreviation).ToList();

            var productsByTest = (
                from p in db.Products
                join o in db.OrderItems on p.Id equals o.ProductId
                select new
                {
                    p.TestId, 
                    p.Id,
                    TestName = p.Test.Name,
                    p.Name,
                    p.PricePerUnit,
                    p.ProductNumber,
                    o.Quantity,
                    o.OrderId
                }).OrderBy(x => x.TestName).ToList();

            var allOrderItems = from ordItems in db.OrderItems
                                where ordItems.OrderId == id
                                select ordItems;

            var orderItems = (
                from ordItem in db.OrderItems
                join product in db.Products on ordItem.ProductId equals product.Id
                select ordItem).OrderBy(x => x.Product.Test.Abbreviation).ToList();

            foreach (var item in productsByTest)
            {
                if (item.OrderId == vm.Order.Id && item.Quantity != 0)
                {
                    OrderDetailsVM.ProductByCompany product = new OrderDetailsVM.ProductByCompany
                    {
                        TestId = item.TestId,
                        TestName = item.TestName,
                        ProductName = item.Name,
                        ProductNumber = item.ProductNumber,
                        Quantity = item.Quantity,
                        PricePerUnit = item.PricePerUnit
                    };
                    //vm.ProductsByCompany.Add(product);
                }

            }


            foreach (var item in allOrderItems)
            {
                item.Product = db.Products.Find(item.ProductId);
                vm.Order.OrderItems.Add(item);
            }

            if (vm.Order == null)
            {
                return HttpNotFound();
            }

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
