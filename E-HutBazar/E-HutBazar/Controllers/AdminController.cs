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
    }
}