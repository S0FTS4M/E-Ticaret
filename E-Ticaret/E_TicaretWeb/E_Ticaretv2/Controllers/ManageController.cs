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
        #region Helpers
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
        async Task<List<Product>> getProductFromFirebase()
        {
            List<Product> products = new List<Product>();
            var pds = await CustomAuth.firebase.Child("Products").OrderByKey().OnceAsync<Product>();
            foreach (var item in pds)
            {
                products.Add(item.Object);
            }
            return products;
        }

        async Task<FirebaseObject<Product>> getProductById(string productId)
        {
            var allProducts =await CustomAuth.firebase.Child("Products").OnceAsync<Product>();
            foreach (var item in allProducts)
            {
                if (item.Object.Id == productId)
                {
                    return item;
                }
            }
            return null;
        }

        async Task<List<FirebaseObject<Comment>>> getCommentsFromFirebase()
        {
            var allComments = await CustomAuth.firebase.Child("Comments").OnceAsync<Comment>();
            List<FirebaseObject<Comment>> comments = new List<FirebaseObject<Comment>>();
            foreach (var item in allComments)
            {
                comments.Add(item);
            }
            return comments;
        }
        #endregion Helpers

        public async Task<ActionResult> Index()
        {
            var user = await getUserFromFirebase(CustomAuth.UserAuth.User.Email);

            var model = new IndexViewModel
            {
                PhoneNumber = user.Object.PhoneNumber,
                Role = user.Object.Role,
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

        [Authorize(Roles ="admin")]
        public ActionResult AddProduct()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var userAuth = CustomAuth.UserAuth;
                FirebaseObject<CustomerAccount> item = await getUserFromFirebase(userAuth.User.Email);
                CustomerAccount acc = item.Object;
                if (model.OldPassword == acc.Pwd)
                {
                    acc.Pwd = model.NewPassword;
                }
                else
                {
                    ViewBag.Message = "Old password is wrong";
                    return View();
                }

                try
                {
                    await CustomAuth.firebase.Child("UserAccount").Child(item.Key).PutAsync(acc);
                    ViewBag.Message = "Password changed";
                }
                catch (Exception)
                {
                    ViewBag.Message = "Password could not changed";
                    return View();
                }
            }
            return View();
        }

        [Authorize(Roles ="admin")]
        [HttpPost]
        public async Task<ActionResult> AddProduct(AddProductViewModel model)
        {
            await CustomAuth.firebase.Child("Products").PostAsync(model);
            return View();
        }

        
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> EditProduct(string productId)
        {
            FirebaseObject<Product> pd = await getProductById(productId);
            ViewBag.product = pd.Object;
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> EditProduct(EditProductViewModel model)
        {
            FirebaseObject<Product> product = await getProductById(model.Id);
            if (product == null)
            {
                ViewBag.Message = "Product could not found";
                return View();
            }
            await CustomAuth.firebase.Child("Products").Child(product.Key).PutAsync(model);
            ViewBag.Message = "Product edited";
            ViewBag.product = product.Object;
            return View();
        }
        
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> ListProduct()
        {
            var model = await getProductFromFirebase(); 
            return View(model);
        }
        
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> ListComment()
        {
            var model = await getCommentsFromFirebase();
            List<string> keys = new List<string>();
            List<Comment> comments = new List<Comment>();
            foreach (var item in model)
            {
                comments.Add(item.Object);
                keys.Add(item.Key);
            }
            ViewBag.comments = comments;
            ViewBag.keys = keys;
            return View();
        }

        [Authorize(Roles ="admin")]
        public async Task<ActionResult> DeleteComment(string commentKey)
        {
            try
            {
                await CustomAuth.firebase.Child("Comments").Child(commentKey).DeleteAsync();
                ViewBag.Message = "Comment Deleted";
            }
            catch
            {
                ViewBag.Message = "Comment could not deleted";
            }
            return RedirectToAction("ListComment");
        }

        [Authorize(Roles ="admin")]
        public async Task<ActionResult> DeleteProduct(string productId)
        {
            FirebaseObject<Product> product = await getProductById(productId);
            if (product == null)
            {
                ViewBag.Message = "Product could not found";
                return RedirectToAction("ListProduct");
            }
            await CustomAuth.firebase.Child("Products").Child(product.Key).DeleteAsync();
            ViewBag.Message = "Product Deleted";
            return RedirectToAction("ListProduct");
        }
    }
}   