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
    public class InventoryController : Controller
    {
        private InventoryContext db = new InventoryContext();       
        //
        // GET: /Inventory/

        public ActionResult Index()
        {
            TakeInventoryVM vm = new TakeInventoryVM();
            vm.Inventories = db.Inventories
                .Include(i => i.InventoryItems)
                .OrderByDescending(x => x.Date)
                .ToList();

            //Check for any currently open inventories and set VM property as such
            //(Necessary to display proper link in index.cshtml
            if (db.Inventories.All(x => x.Completed))
            {
                vm.AllInventoriesComplete = true;
            }
            else
            {
                vm.AllInventoriesComplete = false;
            }

            return View(vm);
        }

        //
        // GET: /Inventory/Details/5

        public ActionResult Details(int id)
        {
            InventoryContext db = new InventoryContext();

            Inventory inventory = db.Inventories.Find(id);
            var allInventoryItems = from invItems in db.InventoryItems
                                    where invItems.InventoryId == id
                                    select invItems;

            foreach (var item in allInventoryItems)
            {
                item.Product = db.Products.Find(item.ProductId);
                inventory.InventoryItems.Add(item);

            }


            if (inventory == null)
            {
                return HttpNotFound();
            }

            return View(inventory);
        }

        //
        // GET: /Inventory/Create

        public ActionResult TakeInventory()
        {
            TakeInventoryVM vm = new TakeInventoryVM();
            Inventory takeInventory = new Inventory();

            vm.Tests = from x in db.Tests
                       orderby x.Abbreviation ascending
                       select x;

            if (db.Inventories.All(x => x.Completed))
            {

                takeInventory.Date = DateTime.Now;

                db.Inventories.Add(takeInventory);
                db.SaveChanges();

                vm.InventoryId = takeInventory.Id;
                return View(vm);
            }

            else
            {
                var inventories = db.Inventories.Select(x => x).ToList();
                var inv = inventories.LastOrDefault();
                vm.InventoryId = inv.Id;

                return View(vm);
            }

        }

        //
        // POST: /Inventory/Create

        [HttpPost]
        public ActionResult TakeInventory(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Inventory/TakeItemInventory
        public ActionResult TakeItemInventory(int inventoryId, int testId)
        {
            TestItemInventoryVM vm = new TestItemInventoryVM();
            var regularlyOrderedProducts = from x in db.Products
                                          where x.TestId==testId && x.RegularlyOrdered == true
                                          select x;

            vm.RegularlyOrderedProducts = regularlyOrderedProducts.ToList();
            vm.InventoryId = inventoryId;
            vm.Test = db.Tests.Find(testId);
            Inventory inventory = db.Inventories.Find(inventoryId);

            return View(vm);
        }

        //
        // POST: /Inventory/TakeItemInventory
        [HttpPost]
        public ActionResult TakeItemInventory(TestItemInventoryVM vm)
        {
            Product product = new Product();
            Inventory inventory = new Inventory();

            foreach (var item in vm.RegularlyOrderedProducts)
            {
                //Map VM to product instance
                product = db.Products.Find(item.Id);
                product.UnitsInStock = item.UnitsInStock;
                db.Entry(product).State = EntityState.Modified;

                //Map vm & product to InventoryItem instance?????
                inventory = db.Inventories.Find(vm.InventoryId);

                var inventoryItem = new InventoryItem()
                {
                    Product = product,
                    Inventory = inventory, 
                    Quantity = product.UnitsInStock
                };
                db.InventoryItems.Add(inventoryItem);                
            }

            db.SaveChanges();

            return RedirectToAction("TakeInventory");
        }

        //
        // /Inventory/ConcludeInventory
        public ActionResult ConcludeInventory()
        {
            var inventories = db.Inventories.Select(x => x).ToList();
            var inv = inventories.LastOrDefault();
            inv.Completed = true;
            db.Entry(inv).State = EntityState.Modified;
            db.SaveChanges();

            //TODO: Create Confirmation Page
            return RedirectToAction("Index");
        }

        //
        // GET: /Inventory/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Inventory/Edit/5

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
        // GET: /Inventory/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Inventory/Delete/5

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
