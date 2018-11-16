using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Ticaret.Models.EntityFramework;
namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

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
            return View();
        }

        public ActionResult Shopping_Cart()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Sign_In(CustomerAccount account)
        {
            E_TicaretEntities2 db = new E_TicaretEntities2();
            if (account.customerUserName == null || account.customerPassword == null || account.customerPassword2 == null ||account.customerEmail ==null)
            {
                return View();
            }
            if(Char.IsDigit(account.customerUserName[0]))
            {
                return View();
            }
            if (account.customerPassword != account.customerPassword2)
            {
                return View();
            }
            // add to database
            db.CustomerAccount.Add(account);
            // save changes
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult Sign_In()
        {
            return View();
        }

        public ActionResult ViewProduct()
        {
            return View();
        }
    }
}