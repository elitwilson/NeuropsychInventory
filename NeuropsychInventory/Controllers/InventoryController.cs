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

            var inventory = db.Inventory.Include(i => i.InventoryItems);
            return View(inventory.ToList());
        }

        //
        // GET: /Inventory/Details/5

        public ActionResult Details(int id)
        {
            InventoryContext db = new InventoryContext();

            Inventory inventory = db.Inventory.Find(id);
            var allInventoryItems = from invItems in db.InventoryItems
                                    where invItems.InventoryId == id
                                    select invItems;

            foreach (var item in allInventoryItems)
            {
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

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Inventory/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
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
