﻿using E_Ticaretv2.Models;
using E_Ticaretv2.ViewModels;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Ticaretv2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        async Task<FirebaseObject<CustomerAccount>> getUserFromFirebase(string email)
        {

            var acc = await CustomAuth.firebase.Child("UserAccount").OnceAsync<CustomerAccount>();
            foreach (var item in acc)
            {
                if (item.Object.EMail == email)
                {
                    return item;
                }
            }
            return null;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                
                return RedirectToAction("Index","Manage");
            }
            else
            {
                return View();
            }

        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel account,string returnUrl)
        {
            FirebaseClient firebase = CustomAuth.firebase;
            if (string.IsNullOrEmpty(account.Email)==false || string.IsNullOrEmpty(account.Password) == false) 
            {

                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBffXQCMpQYkqD1P6WKymTUd2LkfccU2TU"));
                var auth = new FirebaseAuthLink(authProvider, CustomAuth.UserAuth);
                
                try
                {
                    auth = await authProvider.SignInWithEmailAndPasswordAsync(account.Email, account.Password);
                }
                catch
                {
                }
                if (auth.User != null)
                {
                    FormsAuthentication.SetAuthCookie(auth.User.Email, false);
                    CustomAuth.UserAuth = auth;
                    // to get roles for user, we get the logged in user informations
                    FirebaseObject<CustomerAccount> acc = await getUserFromFirebase(auth.User.Email);
                    CustomAuth.loggedInAccount = new CustomerAccount
                    {
                        EMail = auth.User.Email,
                        Role = acc.Object.Role,
                    };
                    return RedirectToAction("Index", "Manage");
                }
 
            }
            ViewBag.Error = "Account is invalid";
            return View("Login");


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            CustomAuth.UserAuth = null;
            CustomAuth.loggedInAccount = null;
            return RedirectToAction("Login","Account");
        }


        [AllowAnonymous]
        public ActionResult Shopping_Cart()
        {
            return RedirectToAction("Shopping_Cart", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                FirebaseObject<CustomerAccount> user = await getUserFromFirebase(model.Email);
                if (user != null)
                {
                    ViewBag.Error = "This e mail address is used";
                    return View();
                }
                var firebase = CustomAuth.firebase;
                FirebaseAuth firebaseAuth = new FirebaseAuth();
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBffXQCMpQYkqD1P6WKymTUd2LkfccU2TU"));
                try
                {
                    var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password);
                    CustomerAccount userModel = new CustomerAccount
                    {
                        EMail = model.Email,
                        Pwd = model.Password,
                        PhoneNumber = model.PhoneNumber,
                        Name = "",
                        Surname="",
                        UserName=model.Email,
                        Role = "",
                    };
                    await firebase.Child("UserAccount").PostAsync(userModel);
                    return RedirectToAction("Login", "Account");
                }
                catch (Exception)
                {
                    ViewBag.Error = "Can not use this e mail address";
                    return View(model);
                }
            }
            ViewBag.Error = "Username or password is not valid";
            return View(model);
        }

    }
}