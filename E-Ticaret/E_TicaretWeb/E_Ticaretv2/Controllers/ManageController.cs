using E_Ticaretv2.Models;

using E_Ticaretv2.ViewModels;
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
    [Authorize]
    public class ManageController : Controller
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
            var user = CustomAuth.UserAuth;
            var model = new IndexViewModel
            {
                PhoneNumber = user.User.PhoneNumber,
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangeInfo()
        {
            return View();
        }

        // POST: /Manage/ChangeInfo
        [HttpPost]
        public async Task<ActionResult> ChangeInfo(ChangeInfoViewModel model)
        {
            FirebaseClient firebase = CustomAuth.firebase;
            if (ModelState.IsValid)
            {
                var userAuth = CustomAuth.UserAuth;
                FirebaseObject<CustomerAccount> item = await getUserFromFirebase(userAuth.User.Email);
                CustomerAccount acc = item.Object;
                // update informations
                if (model.Number != null)
                    acc.PhoneNumber = model.Number;

                if (model.Name != null)
                {
                    if (model.Name.Any(char.IsDigit) == false)
                        acc.Name = model.Name;
                    else
                    {
                        ViewBag.Message = "Name can not have any number";
                        return View();
                    }
                }
                if (model.Surname != null)
                {
                    if (model.Surname.Any(char.IsDigit) == false)
                        acc.Surname = model.Surname;
                    else
                    {
                        ViewBag.Message = "Surname can not have any number";
                        return View();
                    }
                }
                acc.UserName = userAuth.User.Email;
                await firebase.Child("UserAccount").Child(item.Key).PutAsync(acc);

                // print message
                ViewBag.Message = "Changed";
                return View();
            }
            return View();
        }

    }
}   