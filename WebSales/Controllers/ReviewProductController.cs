using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSales.Models;
using WebSales.Models.EF;

namespace WebSales.Controllers
{
    public class ReviewProductController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: ReviewProduct
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _ReviewProduct(int productId)
        {
            ViewBag.ProductId = productId;
            var item = new ReviewProduct();
            if (User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindByName(User.Identity.Name);
                if (user != null)
                {
                    item.Email = user.Email;
                    item.FullName = user.FullName;
                    item.UserName = user.UserName;
                }
                return PartialView(item);
            }
            return PartialView();
        }

        public ActionResult _load_ReviewProduct(int productId)
        {
            var item = _db.ReviewProducts.Where(x=>x.ProductId == productId).OrderBy(x=>x.Id).ToList();
            ViewBag.Count = item.Count;
            return PartialView(item);
        }

        [HttpPost]
        public ActionResult PostReview(ReviewProduct req)
        {
            if(ModelState.IsValid)
            {
                req.CreatedDate = DateTime.Now;
                _db.ReviewProducts.Add(req);
                _db.SaveChanges();
                return Json(new {Success =true});
            }
            return Json(true);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}