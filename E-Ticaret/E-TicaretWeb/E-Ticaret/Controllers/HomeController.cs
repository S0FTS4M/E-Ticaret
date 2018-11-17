using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using E_Ticaret.Models;
using E_Ticaret.Models.EntityFramework;
namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sign_InUp(SignInUp sIU)
        {
            return View(new SignInUp { SignIn = new Sign_In(), SignUp = new CustomerAccount() });
        }

        [HttpPost]
        public ActionResult SignIn(SignInUp account)
        {
            if (ModelState.IsValid)
            {
                using (E_TicaretEntities2 db = new E_TicaretEntities2())
                {
                    // check the username and password
                    var v = db.CustomerAccount.Where(a => a.customerUserName.Equals(account.SignIn.username) && a.customerPassword.Equals(account.SignIn.password)).FirstOrDefault();
                    if (v != null)
                    {
                        // authenticate the user
                        FormsAuthentication.SetAuthCookie(v.customerUserName, false);
                        // go to the main page
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.loginError = "Failed";
                        return View("Sign_InUp");
                    }
                }
            }
            return View("Sign_InUp");
        }
        // log out
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Sign_InUp");
        }
        [HttpPost]
        public ActionResult SignUp(SignInUp account)
        {
            E_TicaretEntities2 db = new E_TicaretEntities2();
            if (account.SignUp.customerUserName == null || account.SignUp.customerPassword == null || account.SignUp.customerPassword2 == null || account.SignUp.customerEmail == null)
            {
                ViewBag.error = "Please fill username passwords and e mail";
                return View("Sign_InUp");
            }
            if (Char.IsDigit(account.SignUp.customerUserName[0]))
            {
                ViewBag.error = "Username can not start with number";
                return View("Sign_InUp");
            }
            if (account.SignUp.customerPassword != account.SignUp.customerPassword2)
            {
                ViewBag.error = "Passwords are different";
                return View("Sign_InUp");
            }
            // add to database
            db.CustomerAccount.Add(account.SignUp);
            // save changes
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                // return the error message
                ViewBag.error = e.Message;
                return View("Sign_InUp");

            }
            ViewBag.error = "Account created!";
            return View("Sign_InUp");
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
            return View();
        }

        public ActionResult Shopping_Cart()
        {
            return View();
        }

        public ActionResult ViewProduct()
        {
            return View();
        }
    }
}