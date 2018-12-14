using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: Cart
        List<Item> cart = new List<Item>();
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(cart);
        }
        
        public ActionResult OrderNow(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                cart.Add(new Item(db.Products.Find(id), quantity));
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
                    cart.Add(new Item(db.Products.Find(id), 1));
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index","ShoppingCart");
        }
        
        public ActionResult Delete(int id)
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