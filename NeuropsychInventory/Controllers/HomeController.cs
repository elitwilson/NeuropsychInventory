using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using NeuropsychInventory.Models;

namespace NeuropsychInventory.Controllers
{
    public class HomeController : Controller
    {
        private InventoryContext db = new InventoryContext();

        public ActionResult Index()
        {
            var lastInventory = db.Inventories
                .OrderByDescending(x => x.Date)
                .Select(q => q.Date)
                .First()
                .ToShortDateString();

            var lastOrder = db.Orders
                .OrderByDescending(x => x.Date)
                .Select(q => q.Date)
                .First()
                .ToShortDateString();

            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            ViewBag.LastInventory = lastInventory;
            ViewBag.LastOrder = lastOrder;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
