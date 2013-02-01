using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NeuropsychInventory.Models;
using NeuropsychInventory.ViewModels;
using NeuropsychInventory.Utilities;

namespace NeuropsychInventory.Controllers
{
    public class TestController : Controller
    {
        private InventoryContext db = new InventoryContext();

        //
        // GET: /Test/

        public ActionResult Index() {
            TestVM vm = new TestVM();
            
            var products = db.Products
                .Include(p => p.Test)
                .OrderBy(p => p.Test.Abbreviation).ToList();

            var allTests = (from t in db.Tests
                            select new {
                                t.Id, t.Abbreviation
                            }).OrderBy(x => x.Abbreviation).ToList();

            foreach (var item in allTests) {
                TestVM.Test test = new TestVM.Test {
                    Id = item.Id, 
                    Abbreviation = item.Abbreviation
                };

                var productsByTestId = from p in products
                              where p.TestId == item.Id
                              select p;

                vm.ProductsByTest.AddRange(productsByTestId);
                vm.Tests.Add(test);
            }

            return View(vm);
        }

        //
        // GET: /Test/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Test/Create

        public ActionResult CreateProduct()
        {
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name");
            return View();
        }

        //
        // POST: /Test/Create

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", product.TestId);
            return View(product);
        }

        //
        //GET: /Test/CreateTest

        public ActionResult CreateTest()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            return View();
        }

        //
        //POST: /Test/CreateTest

        [HttpPost]
        public ActionResult CreateTest(Test test)
        {
            if (ModelState.IsValid)
            {
                db.Tests.Add(test);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            return View(test);
        }

        //
        // GET: /Test/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", product.TestId);
            return View(product);
        }

        //
        // POST: /Test/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", product.TestId);
            return View(product);
        }

        //
        // GET: /Test/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Test/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}