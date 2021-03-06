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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(User_Admin a)
        {
            var user = AdminUserRepository.Authenticate(a.Admin_Username,a.Admin_Password);
            if (user != null)
            {
                var data = AdminUserRepository.Authenticate(a.Admin_Username, a.Admin_Password);
                FormsAuthentication.SetAuthCookie(data.Admin_Username, true);
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult AddAdmin(User_Admin a)
        {
            EhutBazardbEntities db = new EhutBazardbEntities();
            db.User_Admin.Add(a);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        public ActionResult GetAdminUser()
        {
            
            
            EhutBazardbEntities db = new EhutBazardbEntities();
            //var admins = db.User_Admin.ToList();
            var admins = from ad in db.User_Admin
                         where ad.Admin_Username != "jahid"
                         select ad;
            return View(admins);
        }
        
        public ActionResult Update(int id)
        {
            EhutBazardbEntities db = new EhutBazardbEntities();
            var admin = (from ad in db.User_Admin
                         where ad.Admin_Id == id
                         select ad).FirstOrDefault();
            return View(admin);
        }
        [HttpPost]
        public ActionResult Update(User_Admin a)
        {
            using (EhutBazardbEntities db = new EhutBazardbEntities())
            {
                User_Admin entity = (from ad in db.User_Admin
                                     where ad.Admin_Id == a.Admin_Id
                                     select ad).FirstOrDefault();
                db.Entry(entity).CurrentValues.SetValues(a);
                db.SaveChanges();
                return RedirectToAction("GetAdminUser");
            }
        }
        public ActionResult Delete(int id)
        {
            using (EhutBazardbEntities db = new EhutBazardbEntities())
            {
                var admin = (from ad in db.User_Admin
                             where ad.Admin_Id == id
                             select ad).FirstOrDefault();
                return View(admin);
            }
        }
        [HttpPost]
        public ActionResult Delete(User_Admin a)
        {
            using (EhutBazardbEntities db = new EhutBazardbEntities())
            {
                var admin = (from ad in db.User_Admin
                             where ad.Admin_Id == a.Admin_Id
                             select ad).FirstOrDefault();
                db.User_Admin.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
        }
            
    }
}
