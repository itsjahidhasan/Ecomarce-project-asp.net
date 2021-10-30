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
        public ActionResult ProductList()
        {
            EhutBazardbEntities db = new EhutBazardbEntities();
            var data = from pid in db.Shop_Product
                       select pid;

            return View(data.ToList());
        }
        public ActionResult EditProduct(int Id)
        {
            EhutBazardbEntities db = new EhutBazardbEntities();
            var product = (from pid in db.Shop_Product
                           where pid.Product_Id == Id
                           select pid).First();
            return View(product);
        }
        [HttpPost]
        public ActionResult EditProduct(Shop_Product sp)
        {
            using (EhutBazardbEntities db = new EhutBazardbEntities())
            {
                Shop_Product entity = (from pid in db.Shop_Product
                                       where pid.Product_Id == sp.Product_Id
                                       select pid).FirstOrDefault();

                db.Entry(entity).CurrentValues.SetValues(sp);
                db.SaveChanges();
                return RedirectToAction("ProductList");
            }

        }
        public ActionResult Delete(int Id)
        {
            using (EhutBazardbEntities db = new EhutBazardbEntities())
            {
                Shop_Product p = (from pid in db.Shop_Product
                                  where pid.Product_Id == Id
                                  select pid).FirstOrDefault();
                return View(p);
            }
        }
        [HttpPost]
        public ActionResult Delete(Shop_Product sp)
        {
            using (EhutBazardbEntities db = new EhutBazardbEntities())
            {
                Shop_Product entity = (from pid in db.Shop_Product
                                       where pid.Product_Id == sp.Product_Id
                                       select pid).FirstOrDefault();
                db.Shop_Product.Remove(entity);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
        }
    }
}