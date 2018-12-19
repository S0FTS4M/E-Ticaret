
using E_Ticaretv2.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Ticaretv2.Controllers
{
    public class HomeController : Controller
    {
        FirebaseClient firebase = CustomAuth.firebase;
        #region helpers
        async Task<List<Product>> getProductFromFirebase()
        {
            List<Product> products = new List<Product>();
            var pds = await firebase.Child("Products").OrderByKey().OnceAsync<Product>();
            foreach (var item in pds)
            {
                products.Add(item.Object);
            }
            return products;
        }

        #endregion helpers

        public async Task<ActionResult> Index()
        {
            ViewBag.products = await getProductFromFirebase();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CategoryShow(string categoryName, string subCategory)
        {
            if (categoryName == null)
                return View();
            categoryName = categoryName.ToLower();
            List<Product> products = await getProductFromFirebase();
            List<Product> pdForCategory = new List<Product>();

            // if it's none, we know that we will look general category, like sport,casual...
            if (categoryName == "none")
            {
                subCategory = subCategory.ToLower();
                pdForCategory = products.FindAll(x => x.SubCategory.ToLower() == subCategory || x.Category.ToLower() == subCategory);
            }
            // if we have just category name
            else if (subCategory != null)
            {
                subCategory = subCategory.ToLower();
                pdForCategory = products.FindAll(x => x.SubCategory.ToLower() == subCategory && x.Category.ToLower() == categoryName);
            }
            // if we have category name and sub category name
            else
            {
                if (categoryName.ToLower() == "sales")
                {
                    pdForCategory = products.FindAll(x => x.Discount != 0);
                }
                else
                {
                    pdForCategory = products.FindAll(x => x.Category.ToLower() == categoryName.ToLower());
                }
            }
            ViewBag.products = pdForCategory;
            return View();
        }


        public async Task<ActionResult> Search(string SearchProductName)
        {
            List<Product> products = await getProductFromFirebase();
            ViewBag.products = products.Where(x => x.Name.ToLower() == SearchProductName.ToLower());
            if (ViewBag.products == null)
            {
                ViewBag.products = null;
                ViewBag.Message = "Could not find the product";
                return View();
            }
            ViewBag.Message = "Found Product";
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
            return RedirectToAction("Index", "ShoppingCart");
        }

        public async Task<ActionResult> Sales()
        {
            List<Product> products = await getProductFromFirebase();
            List<Product> pd = products.FindAll(x => x.SubCategory == "sales");
            ViewBag.products = pd;
            return View();
        }
        public ActionResult Product()
        {
            return RedirectToAction("List", "Products");
        }
    }
}