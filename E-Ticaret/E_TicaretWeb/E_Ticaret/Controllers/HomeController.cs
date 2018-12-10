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
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
       
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
            
            // we split the categories
            string[] categories = categoryName.Split(',');
          
            // get the product list from database
            var list = db.Products.ToList();
            List<Product> pd = new List<Product>();
            foreach (var item in list)
            {
                int c = 0;
                // get categories of every product
                string[] pdCategory = item.Category.Split(',');
                for (int i = 0; i < categories.Length; i++)
                {
                   // if category name is inside of the product category increasing "c"
                    if (Array.IndexOf(pdCategory,categories[i]) != -1)
                    {
                        c++;
                    }
                }
                // if the length is equal, it means the product is in specific category
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
            return RedirectToAction("Index", "ShoppingCart");
        }

        public ActionResult Sales()
        {
            var list = db.Products.ToList();
            List<Product> pd = new List<Product>();
            foreach (var item in list)
            {
                string[] categories = item.Category.Split(',');
                for (int i = 0; i < categories.Length; i++)
                {
                    if (categories[i] == "sales")
                        pd.Add(item);
                }
            }
            return View(pd);
        }
        public ActionResult Product()
        {
            return RedirectToAction("List", "Products");
        }
    }
}