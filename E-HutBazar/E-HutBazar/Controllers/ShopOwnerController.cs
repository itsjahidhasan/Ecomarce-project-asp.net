using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using E_HutBazar.Models;
using E_HutBazar.Repository;

namespace E_HutBazar.Controllers
{
    public class ShopOwnerController : Controller
    {
        // GET: ShopOwner
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(ShopOwner s)
        {
            var user = ShopOwnerUserRepository.Authenticate(s.ShopO_Username, s.ShopO_Password);
            if (user != null)
            {
                var data = ShopOwnerUserRepository.Authenticate(s.ShopO_Username, s.ShopO_Password);
                FormsAuthentication.SetAuthCookie(data.ShopO_Username, true);
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddShopOwner(Request_Shopowner rsh)
        {
            EhutBazardbEntities db = new EhutBazardbEntities();
            db.Request_Shopowner.Add(rsh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddProducts()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProducts(Shop_Product sp)
        {
            EhutBazardbEntities db = new EhutBazardbEntities();
            db.Shop_Product.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}