using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Ticaret.Models;
using Microsoft.Owin;

namespace E_Ticaret.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Products
        public ActionResult Index()
        {
            
            return View(db.Products.ToList());
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddComment(string userComment, int productId,string userName)
        {
            userComment = userComment.Trim();
            if (userComment == null || userComment == "")
                return RedirectToAction("Details", new { id = productId });

            // creating new comment with infos
            Comment comment = new Comment
            {
                productId = productId,
                commentText = userComment,
                commentDislikeCount = 0,
                commentLikecount = 0,
                customerUserName = userName,
            };
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = productId});
        }
        
        [Authorize]
        public ActionResult DeleteComment(int commId, int productId)
        {
            // sql query for comments
            List<Comment> comment = db.Comments.SqlQuery("select * FROM dbo.Comments WHERE productId= @id", new SqlParameter("@id",productId )).ToList();
            for (int i = 0; i < comment.Count; i++)
            {
                if (comment.ElementAt(i).commentId == commId)
                {
                   // delete comment 
                    db.Comments.Remove(comment.ElementAt(i));
                    db.SaveChanges();
                    break;
                }
            }
            return RedirectToAction("Details", new { id = productId });
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // find product
            Product product = db.Products.Find(id);

            
            if (product == null)
            {
                return HttpNotFound();
            }

            // find all comments with product id
            List<Comment> comment = db.Comments.SqlQuery("select * FROM dbo.Comments WHERE productId= @id", new SqlParameter("@id", id)).ToList();
            product.comment = comment;


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

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Amount,Image,Date,Price,Description,Category,mainPageUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Amount,Image,Date,Price,Description,Category,InMainPage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
