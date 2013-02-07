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
              
        //
        // GET: /Inventory/

        public ActionResult Index()
        {
            InventoryContext db = new InventoryContext(); 
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
            InventoryContext db = new InventoryContext();
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
            InventoryContext db = new InventoryContext();
            TestItemInventoryVM vm = new TestItemInventoryVM();

            var regularlyOrderedProducts = (from p in db.Products
                                  where p.TestId == testId && p.RegularlyOrdered == true
                                  select p).ToList();

            var invItems = (from i in db.InventoryItems
                            where i.Product.TestId == testId && i.InventoryId == inventoryId
                            select i).ToList();

            foreach (var item in regularlyOrderedProducts) {
                TestItemInventoryVM.Product p = new TestItemInventoryVM.Product {
                    Id = item.Id,
                    Name = item.Name,
                    MaxInStock = item.MaxInStock,
                    UnitsInStock = item.UnitsInStock
                };

                var invItem = (from i in invItems
                               where i.ProductId == p.Id
                               select i).FirstOrDefault();

                if (invItem != null) {
                    p.IsInventoried = true;
                }
                vm.RegularlyOrderedProducts.Add(p);
            }

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
            InventoryContext db = new InventoryContext();
            Inventory inventory = db.Inventories.Find(vm.InventoryId);

            foreach (var item in vm.RegularlyOrderedProducts) {
                Product product = db.Products.Find(item.Id);
                var retrievedInvItem = (from i in db.InventoryItems
                                    where i.ProductId == product.Id && i.InventoryId == inventory.Id
                                    select i).FirstOrDefault();
                
                //Ensure Quantity has changed before trying to set values                
                if (item.UnitsInStock != product.UnitsInStock) {
                    InventoryItem invItem = new InventoryItem {
                        Product = product,
                        ProductId = product.Id,
                        Inventory = inventory,
                        InventoryId = inventory.Id,
                        Quantity = item.UnitsInStock,
                        IsInventoried = true
                    };

                    //Set UnitsInStock on Product
                    product.UnitsInStock = item.UnitsInStock;
                    db.Entry(product).State = EntityState.Modified;

                    //If record exists, update it.
                    if (retrievedInvItem != null) {
                        db.Entry(retrievedInvItem).CurrentValues.SetValues(invItem);
                    }

                    //If record doesn't exist, add it
                    else {
                        db.InventoryItems.Add(invItem);
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("TakeInventory");
        }

        //
        // /Inventory/ConcludeInventory
        public ActionResult ConcludeInventory()
        {
            InventoryContext db = new InventoryContext();
            var inventories = db.Inventories.Select(x => x).ToList();
            var inv = inventories.LastOrDefault();
            inv.Completed = true;

            //Need to reset IsInventoried values all to 'false'
            foreach (var item in db.InventoryItems.Where(x => x.InventoryId == inv.Id)) {
                item.IsInventoried = false;
            }

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
