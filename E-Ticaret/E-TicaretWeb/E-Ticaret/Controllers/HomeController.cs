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
        public ActionResult Sign_In()
        {
            return View();
        }
    }
}