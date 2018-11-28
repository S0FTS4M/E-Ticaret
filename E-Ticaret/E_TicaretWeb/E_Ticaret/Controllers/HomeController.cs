using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();       
            return View(db.Products.ToList());
        }

        [Authorize]
        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult Account()
        {
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Shopping_Cart()
        {
            return View();
        }

        public ActionResult ViewProduct()
        {
            return View();
        }
        public ActionResult Sales()
        {
            return View();
        }
        public ActionResult Product()
        {
            return RedirectToAction("List", "Products");
        }
    }
}