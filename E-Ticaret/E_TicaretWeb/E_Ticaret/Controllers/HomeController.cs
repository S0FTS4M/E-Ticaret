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
        public ActionResult CategoryShow(string categoryName)
        {
            if (categoryName == null)
                return View();
            string[] categories = categoryName.Split(',');
            ApplicationDbContext db = new ApplicationDbContext();
            var list = db.Products.ToList();
            List<Product> pd = new List<Product>();
            foreach (var item in list)
            {
                int c = 0;
                string[] pdCategory = item.Category.Split(',');
                for (int i = 0; i < categories.Length; i++)
                {
                    if (Array.IndexOf(pdCategory,categories[i]) != -1)
                    {
                        c++;
                    }
                }
                if (c == categories.Length)
                    pd.Add(item);
            }

           
            return View(pd);
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