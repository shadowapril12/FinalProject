using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FinalProject.Models;
using Newtonsoft.Json;

namespace FinalProject.Controllers
{
    public class AuthorisationController : Controller
    {
        TestingContext db = new TestingContext();
        // GET: Authorisation
        public ActionResult Index()
        {
            User user1 = new User();

            return View(user1);
        }

        [HttpPost]
        public ActionResult CheckUser(User user)
        {
            string l = user.Login;

            User u = db.Users.Where(us => us.Login == user.Login).FirstOrDefault();

            if(u != null)
            {
                if(u.RoleId == 1)
                {
                    return RedirectToAction("Index", "Home", new { userId = u.Id });
                }
                else
                {
                    return RedirectToAction("GuestIndex", "Home", new { userId = u.Id});
                }
            }
            else
            {
                u.Login = user.Login;
                u.Role = db.Roles.Where(r => r.RoleType == "user").FirstOrDefault();
                u.RoleId = u.Role.Id;

                string userSerialized = JsonConvert.SerializeObject(u);
                return RedirectToAction("AddUser", "Authorisation", new { user = userSerialized });
            }
        }

        public ActionResult AddUser(string user)
        {
            User u = JsonConvert.DeserializeObject<User>(user);
            db.Users.Add(u);

            db.SaveChanges();

            return RedirectToAction("GuestIndex", "Home", new { userId = u.Id });
        }

        //Сборщик мусора
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}