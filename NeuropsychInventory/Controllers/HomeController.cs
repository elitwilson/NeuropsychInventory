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
            //Trying to query date of most recently taken Inventory
            //var q = from n in db.Inventory
            //        group n by n.Id into g
            //        select new { Date = g.Min(t => t.Date) };

            //var lastInventory = q.FirstOrDefault();

            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";
            

            ViewBag.LastInventory = "NA";
            ViewBag.LastOrder = "NA";

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
