using E_Ticaretv2.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaretv2.Controllers
{
    public class ShoppingCartController : Controller
    {
        List<Item> cart = new List<Item>();
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
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ViewBag.cart = cart;
            return View();
        }

        public async Task<ActionResult> OrderNow(string id, int quantity)
        {
            List<Product> products = await getProductFromFirebase();
            if (Session["cart"] == null)
            {
                cart.Add(new Item(products.Find(x => x.Id == id), quantity));
                Session["cart"] = cart;
            }
            else
            {
                bool exist = false;
                List<Item> cart = (List<Item>)Session["cart"];
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].Pd.Id == id)
                    {
                        cart[i].Quantity += quantity;
                        exist = true;
                        break;
                    }
                }
                if (exist == false)
                {
                    cart.Add(new Item(products.Find(x => x.Id == id), 1));
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        public ActionResult Delete(string id)
        {
            cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Pd.Id == id)
                {
                    cart[i].Quantity -= 1;
                    if (cart[i].Quantity == 0)
                        cart.Remove(cart[i]);
                }
            }
            Session["cart"] = cart;
            return View("Cart");
        }
    }
}