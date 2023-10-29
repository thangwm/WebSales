using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSales.Models;

namespace WebSales.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NavbarMenu()
        {
            var items = db.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_NavbarMenu", items);
        }

        public ActionResult Banner()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_Banner", items);
        }

        public ActionResult SidebarCategories()
        {            
            var items = db.ProductCategories.ToList();
            return PartialView("_SidebarCategories", items);
        }      

        public ActionResult NewArrivals()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_NewArrivals", items);
        }

    }
}