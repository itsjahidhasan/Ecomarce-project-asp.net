using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_HutBazar.Models;

namespace E_HutBazar.Repository
{
    public class AdminUserRepository
    {
        static EhutBazardbEntities db;
        static AdminUserRepository()
        {
            db = new EhutBazardbEntities();
        }
        public static User_Admin Authenticate(string userName, string password)
        {
            var c = (from ad in db.User_Admin
                     where ad.Admin_Username == userName &&
                     ad.Admin_Password == password
                     select ad).FirstOrDefault();

            
            return c;
        }
    }
}