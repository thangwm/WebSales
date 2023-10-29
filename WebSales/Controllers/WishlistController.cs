using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Services.Description;
using System.Web.UI;
using WebSales.Models;
using WebSales.Models.EF;

namespace WebSales.Controllers
{
    public class WishlistController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Wishlist
        public ActionResult Index(int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Wishlist> items = db.Wishlists.Where(x => x.Username == User.Identity.Name).OrderByDescending(x => x.CreatedTime);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        [HttpPost]
        public ActionResult PostWishlist ( int ProductId)
        {
            if(Request.IsAuthenticated == false)
            {
                return Json(new {Success = false,Message = "No Login" });
            }
            var checkItem = db.Wishlists.FirstOrDefault(x => x.ProductId == ProductId && x.Username == User.Identity.Name);
            if (checkItem != null)
            {
                return Json(new { Success = false, Message = "Product'Favorite" });
            }
            var item = new Wishlist();
            item.ProductId = ProductId;
            item.Username = User.Identity.Name;
            item.CreatedTime = DateTime.Now;
            db.Wishlists.Add(item);
            db.SaveChanges();
            return Json(new {Success = true});  
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Delete(int ProductId)
        {
            var checkItem = db.Wishlists.FirstOrDefault(x => x.ProductId == ProductId && x.Username == User.Identity.Name);
            if (checkItem != null)
            {
                db.Wishlists.Remove(checkItem);
                db.SaveChanges();
                return Json(new { Success = true, Message = "Delete Product'Favorite Success" });
            }
            return Json(new { Success = false, Message = "Delete Product'Favorite Fail" });
        }
    }
}