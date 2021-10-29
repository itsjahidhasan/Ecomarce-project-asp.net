using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_HutBazar.Models;

namespace E_HutBazar.Repository
{
    public class ShopOwnerUserRepository
    {
        static EhutBazardbEntities db;
        static ShopOwnerUserRepository()
        {
            db = new EhutBazardbEntities();
        }
        public static ShopOwner Authenticate(string username, string password)
        {
            var c = (from sh in db.ShopOwners
                     where sh.ShopO_Username == username &&
                     sh.ShopO_Password == password
                     select sh).FirstOrDefault();


            return c;
        }
    }
    
}