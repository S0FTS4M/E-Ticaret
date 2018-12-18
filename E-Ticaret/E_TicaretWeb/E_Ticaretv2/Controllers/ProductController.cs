using E_Ticaretv2.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaretv2.Controllers
{
    public class ProductController : Controller
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
        async Task<FirebaseObject<Comment>> getCommentThatWillDeleted(string commentId)
        {
            var allComments = await firebase.Child("Comments").OnceAsync<Comment>();
            foreach (var item in allComments)
            {
                if (item.Key == commentId)
                {
                    return item;
                }
            }
            return null;
        }
        async Task<List<Comment>> getProductsCommentFromFirebase(string productId)
        {
            var allComments = await firebase.Child("Comments").OnceAsync<Comment>();
            List<Comment> productsComment = new List<Comment>();
            foreach (var item in allComments)
            {
                if (item.Object.ProductId == productId)
                {
                    item.Object.CommentId = item.Key;
                    productsComment.Add(item.Object);
                }
            }
            return productsComment;
        }
        #endregion helpers
        

        // GET: Product
        public async Task<ActionResult> Index()
        {
            ViewBag.products = await getProductFromFirebase();
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddComment(string userComment, string productId, string userName)
        {
            userComment = userComment.Trim();
            if (userComment == null || userComment == "")
                return RedirectToAction("Details", new { id = productId });

            // creating new comment with infos
            Comment comment = new Comment
            {
                ProductId = productId,
                CommentText = userComment,
                DislikeCount = 0,
                LikeCount = 0,
                CustomerUserName = userName,
                CommentId = "",
            };
            await firebase.Child("Comments").PostAsync(comment);
            return RedirectToAction("Details", new { id = productId });
        }

        [Authorize]
        public async Task<ActionResult> DeleteComment(string commentId,string productId)
        {
            // get comments
            FirebaseObject<Comment> comment = await getCommentThatWillDeleted(commentId);
            if (comment== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // delete comment
            await firebase.Child("Comments").Child(comment.Key).DeleteAsync();
            return RedirectToAction("Details", new { id = productId });
        }


        // GET: Products/Details/5
        public async Task<ActionResult> Details(string id="")
        {
            List<Product> pd = await getProductFromFirebase();
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // find product
            Product product = pd.Find(x => x.Id.Equals(id));


            if (product == null)
            {
                return HttpNotFound();
            }

            // find all comments with product id

            product.comment = await getProductsCommentFromFirebase(id);
            return View(product);
        }
        public ActionResult Category()
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}